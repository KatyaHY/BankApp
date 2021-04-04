using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BeTheBank
{
    public partial class details : System.Web.UI.Page
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        private int cnt = 0;
        private bool checkDate = true;
        Regex regex = new Regex(@"\s{2,}");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var prevPage = Request.UrlReferrer.ToString();

                if (prevPage.Contains("login.aspx"))
                {
                    var userID = (string)Session["userID"];
                    string userName;
                    DateTime dt;
                    string date;

                    using (SqlCommand cmd = new SqlCommand("select * from Users where ת_ז=@userID", connection))
                    {
                        connection.Open();
                        cmd.Parameters.AddWithValue("@userID", userID);
                        using (SqlDataReader da = cmd.ExecuteReader())
                        {
                            da.Read();
                            userName = da.GetValue(2).ToString() + " " + da.GetValue(3).ToString();
                            if (da.GetValue(9).ToString() == "")
                            {
                                date = "";
                            }
                            else
                            {
                                dt = DateTime.Parse(da.GetValue(9).ToString());
                                date = String.Format("{0:dd/MM/yyyy}", dt);
                            }

                        }

                    }

                    connection.Close();

                    string message;
                    if (string.IsNullOrEmpty(date))
                    {
                        message = "שלום, " + userName + "! " + "זאת הכניסה הראשונה שלך.";
                    }
                    else
                    {
                        message = "שלום, " + userName + "! כניסה אחרונה: " + date;
                    }

                    string title = "ברוכים הבאים";
                    MessageBoxButtons button = MessageBoxButtons.OK;
                    DialogResult result = MessageBox.Show(message, title, button);
                    if (result == DialogResult.OK)
                    {
                        Set_Login_Date();
                        Load_Form();
                    }
                }
                if (prevPage.Contains("acounntsDetails.aspx"))
                {
                    Load_Form();
                }

            }
        }

        protected void Set_Login_Date()
        {
            idNum.Text = (string)Session["userID"];
            DateTime date = DateTime.Now;
            using (SqlCommand cmdLoginDate = new SqlCommand("update Users set כניסה_אחרונה=@date where ת_ז=@id", connection))
            {
                connection.Open();
                cmdLoginDate.Parameters.AddWithValue("@Id", idNum.Text);
                cmdLoginDate.Parameters.AddWithValue("@date", date.ToString(("yyyy-MM-ddTHH:mm:sszzz")));
                cmdLoginDate.ExecuteNonQuery();
                connection.Close();
            }
        }
        protected void Load_Form()
        {
            idNum.Text = (string)Session["userID"];

            try
            {
                using (SqlCommand cmdShowUserData = new SqlCommand("select * from Users where ת_ז=@idNum", connection))
                {
                    connection.Open();
                    cmdShowUserData.Parameters.AddWithValue("@idNum", idNum.Text);
                    SqlDataReader da = cmdShowUserData.ExecuteReader();
                    while (da.Read())
                    {

                        idNum.Text = da.GetValue(0).ToString();
                        email.Text = da.GetValue(1).ToString();
                        fName.Text = da.GetValue(2).ToString();
                        sName.Text = da.GetValue(3).ToString();
                        phoneNum.Text = da.GetValue(4).ToString();
                        if (!(string.IsNullOrEmpty(da.GetValue(5).ToString())))
                        {
                            DateTime bt = DateTime.Parse(da.GetValue(5).ToString());
                            savedBirthDate.Text = String.Format("{0:dd/MM/yyyy}", bt);
                            birthDate.Visible = false;
                            savedBirthDate.Visible = true;
                        }                       
                        compName.Text = da.GetValue(6).ToString();
                        compNum.Text = da.GetValue(7).ToString();

                    }
                    connection.Close();
                }

            }
            catch (SystemException ex)
            {
                errorLabel.Visible = true;
                errorLabel.Text = (string.Format("ERROR: {0}", ex.Message));
            }


        }
        protected bool Check_Date(String birthDate)
        {
            DateTime dt = DateTime.Parse(birthDate);
            DateTime today = DateTime.Now;
            int countAge = today.Year - dt.Year;
            if (countAge >= 0 && countAge < 120)
            {
                if (today.Month == dt.Month)
                {
                    if (today.Day >= dt.Day)
                    {
                        countAge += 1;
                    }
                }
                else if (today.Month > dt.Month)
                {
                    countAge += 1;
                }
            }


            if (countAge > 120 || countAge <= 0)
            {
                errorBirthDateLabel.Text = "תאריך לא תקין";
                return false;
            }
            else if (countAge < 18)
            {
                errorBirthDateLabel.Text = "עליך להיות מעל גיל 18";
                return false;
            }
            else
            {
                return true;
            }
        }

        protected bool Check_TextBox()
        {
            if (string.IsNullOrEmpty(birthDate.Text) || string.IsNullOrWhiteSpace(birthDate.Text))
            {
                errorBirthDateLabel.Visible = true;
                errorBirthDateLabel.Text = "שדה חובה";
                checkDate = false;
                cnt++;
            }

            if (checkDate && !(Check_Date(birthDate.Text)))
            {
                errorBirthDateLabel.Visible = true;
                cnt++;
            }

            if (string.IsNullOrEmpty(compName.Text) || string.IsNullOrWhiteSpace(compName.Text))
            {
                Response.Write("error");
                errorCompNameLabel.Visible = true;
                errorCompNameLabel.Text = "שדה חובה";
                cnt++;
            }
            if (string.IsNullOrEmpty(compNum.Text) || string.IsNullOrWhiteSpace(compNum.Text))
            {
                errorCompNumLabel.Visible = true;
                errorCompNumLabel.Text = "שדה חובה";
                cnt++;
            }
            if (!(Is_Numeric(compNum.Text)) && !((string.IsNullOrEmpty(compNum.Text) || string.IsNullOrWhiteSpace(compNum.Text))))
            {
                errorCompNumLabel.Visible = true;
                errorCompNumLabel.Text = "אנא הכנס/י רק מספרים";
                cnt++;
            }
            if (!(Is_Numeric(phoneNum.Text)) && !((string.IsNullOrEmpty(phoneNum.Text) || string.IsNullOrWhiteSpace(phoneNum.Text))))
            {
                errorPhoneNum.Visible = true;
                errorPhoneNum.Text = "אנא הכנס/י רק מספרים";
                cnt++;
            }
            if (cnt != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected bool Is_Numeric(string str)
        {
            int num;
            return int.TryParse(str, out num);
        }
        protected void Details_Click(object sender, EventArgs e)
        {
            idNum.Text = (string)Session["userID"];

            errorBirthDateLabel.Visible = false;
            errorPhoneNum.Visible = false;
            errorCompNameLabel.Visible = false;
            errorCompNumLabel.Visible = false;

            if (!(Check_TextBox()))
            {
                Load_Form();
            }

            else
            {
                try
                {

                    using (SqlCommand cmdUpdateUserData = new SqlCommand("update Users set מספר_טלפון=@phNum,תאריך_לידה=@bDate, שם_העסק=@cName, ח_פ=@cNum where ת_ז=@idNum", connection))
                    {
                        DateTime date = DateTime.Now;
                        DateTime bd = DateTime.Parse(birthDate.Text);
                        connection.Open();
                        cmdUpdateUserData.Parameters.AddWithValue("@idNum", idNum.Text);
                        cmdUpdateUserData.Parameters.AddWithValue("@bDate", bd);
                        cmdUpdateUserData.Parameters.AddWithValue("@cNum", int.Parse(compNum.Text));
                        if (string.IsNullOrEmpty(phoneNum.Text) || string.IsNullOrWhiteSpace(phoneNum.Text))
                        {
                            cmdUpdateUserData.Parameters.AddWithValue("@phNum", DBNull.Value);
                        }
                        else
                        {
                            cmdUpdateUserData.Parameters.AddWithValue("@phNum", int.Parse(phoneNum.Text));
                        }
                        if (regex.IsMatch(compName.Text))
                        {
                            compName.Text = Regex.Replace(compName.Text, @"\s+", " ");
                        }
                        cmdUpdateUserData.Parameters.AddWithValue("@cName", compName.Text);
                        cmdUpdateUserData.ExecuteNonQuery();

                        connection.Close();

                        Session["userID"] = idNum.Text;
                        Response.Redirect("acounntsDetails.aspx");
                    }

                }
                catch (SystemException ex)
                {
                    errorLabel.Visible = true;
                    errorLabel.Text = (string.Format("ERROR: {0}", ex.Message));
                }

            }

        }
    }
}   

    

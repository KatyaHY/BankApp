using System;
using System.Configuration;
using System.Data.SqlClient;


namespace BeTheBank
{
    public partial class login : System.Web.UI.Page
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected bool Is_Numeric(string str)
        {
            int num;
            return int.TryParse(str, out num);
        }
        protected void Login_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(id.Text) || string.IsNullOrEmpty(password.Text) || string.IsNullOrWhiteSpace(id.Text) || string.IsNullOrWhiteSpace(password.Text))
            {
                errorLabel.Visible = true;
                errorLabel.Text = "אנא הכנס/י את כל הפרטים";
            }
            else if (!(Is_Numeric(id.Text)))
            {
                errorLabel.Visible = true;
                errorLabel.Text = "יש להכניס רק מספרים";
            }
            else
            {
                try
                {
                    string enteredPassword;
                    using (SqlCommand cmdCheckPassword = new SqlCommand("select * from Users where ת_ז=@id", connection))
                    {
                        connection.Open();
                        cmdCheckPassword.Parameters.AddWithValue("@id", id.Text);
                        using (SqlDataReader da = cmdCheckPassword.ExecuteReader())
                        {
                            da.Read();
                            enteredPassword = da.GetValue(8).ToString().TrimEnd();
                        }
                        if (BCrypt.Net.BCrypt.Verify(password.Text, enteredPassword))
                        {
                            connection.Close();
                            Session["userID"] = id.Text;
                            Response.Redirect("details.aspx");
                        }
                        else
                        {
                            connection.Close();
                            errorLabel.Visible = true;
                            errorLabel.Text = "תעודת זהות או סיסמה לא נכונה";
                        }
                    }
                        
                }
                catch (SystemException ex)
                {
                    errorLabel.Visible = true;
                    errorLabel.Text = (string.Format("ERRORcmdCheckUser: {0}", ex.Message));
                }
            }
          
              
        }
    }
}
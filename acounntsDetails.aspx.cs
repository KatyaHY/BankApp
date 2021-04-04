using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;


namespace BeTheBank
{
    public partial class AcounntsDetails : System.Web.UI.Page
    {

        List<String> bankList = new List<string>();
        List<String> accountList = new List<string>();
        List<String> branchList = new List<string>();
        List<String> btnList = new List<String>();

        protected bool clearInputCheck = true;

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Dropdown_Handler();
                var userID = (string)Session["userID"];

                using (SqlCommand cmd = new SqlCommand("select * from Users where ת_ז=@userID", connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@userID", userID);
                    using (SqlDataReader da = cmd.ExecuteReader())
                    {
                        da.Read();
                        lbCompNum.Text = da.GetValue(7).ToString();
                        lbCompName.Text = da.GetValue(6).ToString();
                    }

                }
                connection.Close();
                Load_Form();
            }

            if (IsPostBack)
            {
                var eTarget = Request.Params["__EVENTTARGET"].ToString();
                Control control = null;
                if (string.Equals(eTarget, btnAdd.ID))
                {
                    Add_Click(btnAdd, null);

                    Load_Form();

                }
                if (string.IsNullOrEmpty(eTarget))
                {
                    Load_Form();

                    string controlId;
                    Control foundControl;

                    foreach (string ctl in Page.Request.Form)
                    {
                        if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                        {
                            controlId = ctl.Substring(0, ctl.Length - 2);
                            foundControl = Page.FindControl(controlId);
                        }
                        else
                        {
                            foundControl = Page.FindControl(ctl);
                        }
                        if (!(foundControl is Button))
                            continue;
                        else
                        {
                            control = foundControl;
                            break;
                        }

                    }
                    if(control == btnBack)
                    {
                        Back_Click(control, null);
                    }
                    else if(control == btnNext)
                    {
                        Next_Click(control, null);
                    }
                    else
                    {
                        Delete_Click(control, null);
                        Response.Redirect("acounntsDetails.aspx");
                    }
                   
                }
                
            }
 
        }  
        
        protected void Dropdown_Handler()
        {
            percentageHold.Items.AddRange(Enumerable.Range(1, 101).Select(e => new ListItem(e.ToString())).ToArray());
        }

        protected void Clear_Input()
        {
            account.Text = string.Empty;
            branch.Text = string.Empty;
            bank.Text = string.Empty;
        }

        protected bool Ckeck_Input()
        {
            var userID = (string)Session["userID"];

            
            using (SqlCommand cmd = new SqlCommand("select * from UserBankAccounts where ח_פ = @cNum and ת_ז = @IDnum and חשבון = @account and בנק=@bank", connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue("@cNum", lbCompNum.Text);
                cmd.Parameters.AddWithValue("@IDnum", userID);
                cmd.Parameters.AddWithValue("@account", account.Text);
                cmd.Parameters.AddWithValue("@bank", bank.Text);

                if (cmd.ExecuteScalar() != null)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }                   
            
        }

        protected void Enter_Values_To_db()
        {
            var userID = (string)Session["userID"];

            using (SqlCommand cmd = new SqlCommand("insert into UserBankAccounts (ח_פ, ת_ז,בנק,סניף,חשבון,החזקה) values (@cNum, @IDnum,@bank,@branch,@account,@percent)", connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue("@cNum", lbCompNum.Text);
                cmd.Parameters.AddWithValue("@IDnum", userID);
                cmd.Parameters.AddWithValue("@bank", bank.Text);
                cmd.Parameters.AddWithValue("@branch", branch.Text);
                cmd.Parameters.AddWithValue("@account", account.Text);
                cmd.Parameters.AddWithValue("@percent", percentageHold.Text);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            using (SqlCommand cmdRowNumber = new SqlCommand("declare @id int set @id=0 update UserBankAccounts set @id = מספר_שורה = @id+1", connection))
            {
                connection.Open();
                cmdRowNumber.ExecuteNonQuery();
                connection.Close();
            }
        }

        protected bool Is_Numeric(string str)
        {
            int num;
            return int.TryParse(str, out num);
        }

        protected void Delete_Values_From_db(Button btn)
        {
            try
            {
                using (SqlCommand cmdDelete = new SqlCommand("delete from UserBankAccounts where מספר_שורה=@buttonID", connection))
                {
                    connection.Open();
                    cmdDelete.Parameters.AddWithValue("@buttonID", btn.ID);
                    cmdDelete.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SystemException ex)
            {
                errorBranchLabel.Visible = true;
                errorBranchLabel.Text = (string.Format("ERROR: {0}", ex.Message));
            }


        }
        protected void Load_Form()
        {
            var userID = (string)Session["userID"];

            using (SqlCommand cmd1 = new SqlCommand("select * from UserBankAccounts where ח_פ=@cNum and ת_ז=@IDnum", connection))
            {
                connection.Open();
                cmd1.Parameters.AddWithValue("@cNum", lbCompNum.Text);
                cmd1.Parameters.AddWithValue("@IDnum", userID);

                using (SqlDataReader da = cmd1.ExecuteReader())
                {
                    while (da.Read())
                    {
                        bankList.Add(da.GetValue(0).ToString());
                        branchList.Add(da.GetValue(1).ToString());
                        accountList.Add(da.GetValue(2).ToString());
                        btnList.Add(da.GetValue(6).ToString());
                    }
                }
                connection.Close();
               
            }
                Dynamic_Func();
            
        }

        protected void Dynamic_Func()
        {
            for (int j = 0; j < accountList.Count(); j++)
            {

                insertForm.Controls.Add(new LiteralControl("<div><br /><div>"));
                HtmlLink styleLink1 = new HtmlLink();
                styleLink1.Attributes.Add("rel", "stylesheet");
                styleLink1.Attributes.Add("type", "text/css");
                styleLink1.Href = "StyleSheet2.css";
                Page.Header.Controls.Add(styleLink1);

                HtmlLink styleLink2 = new HtmlLink();
                styleLink2.Attributes.Add("rel", "stylesheet");
                styleLink2.Href = "https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/css/bootstrap.min.css";
                Page.Header.Controls.Add(styleLink2);

                HtmlLink styleLink3 = new HtmlLink();
                styleLink3.Attributes.Add("rel", "stylesheet");
                styleLink3.Href = "https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/js/bootstrap.bundle.min.js";
                Page.Header.Controls.Add(styleLink3);

                PlaceHolder h = new PlaceHolder();
                Label banklb = new Label();
                Label accountlb = new Label();
                Label branchlb = new Label();
                DropDownList dropD = new DropDownList();
                ListItem bankListItem = new ListItem();
                TextBox textBoxAccount = new TextBox();
                TextBox textBoxBranch = new TextBox();
                Button btn = new Button();
        
                banklb.Text = ":בנק";
                banklb.ApplyStyleSheetSkin(Page);
                banklb.CssClass = "input-group-text";
                banklb.Style["border-color"] = "forestgreen";
                banklb.Style["background"] = "white";
                banklb.Style["border-radius"] = "0px 10px 10px 0px";

                accountlb.Text = "חשבון:";
                accountlb.ApplyStyleSheetSkin(Page);
                accountlb.CssClass = "input-group-text";
                accountlb.Style["border-color"] = "forestgreen";
                accountlb.Style["background"] = "white";
                accountlb.Style["border-radius"] = "0px 10px 10px 0px";

                branchlb.Text = "סניף:";
                branchlb.ApplyStyleSheetSkin(Page);
                branchlb.CssClass = "input-group-text";
                branchlb.Style["border-color"] = "forestgreen";
                branchlb.Style["background"] = "white";
                branchlb.Style["border-radius"] = "0px 10px 10px 0px";

                bankListItem.Text = bankList[j];

                dropD.Items.Add(bankListItem);
                dropD.ID = "dropList" + j.ToString();
                dropD.ApplyStyleSheetSkin(Page);
                dropD.CssClass = "input-group-text";
                dropD.Style["border-color"] = "forestgreen";
                dropD.Style["background"] = "white";
                dropD.Style["border-radius"] = "10px 0px 0px 10px";
                dropD.Style["width"] = "50%";

                textBoxAccount.Text = accountList[j];
                textBoxAccount.ID = "txtBoxAccount" + j.ToString();
                textBoxAccount.ApplyStyleSheetSkin(Page);
                textBoxAccount.CssClass = "form-control";
                textBoxAccount.Style["border-color"] = "forestgreen";
                textBoxAccount.Style["border-radius"] = "10px 0px 0px 10px";
                textBoxAccount.Style["width"] = "50%";

                textBoxBranch.Text = branchList[j];
                textBoxBranch.ID = "txtBoxBranch" + j.ToString();
                textBoxBranch.ApplyStyleSheetSkin(Page);
                textBoxBranch.CssClass = "form-control";
                textBoxBranch.Style["border-color"] = "forestgreen";
                textBoxBranch.Style["border-radius"] = "10px 0px 0px 10px";
                textBoxBranch.Style["width"] = "50%";

                btn.Text = btnDelete.Text;
                btn.ID=btnList[j];
                btn.ApplyStyleSheetSkin(Page);
                btn.CssClass = "btn btn-link";


                h.ApplyStyleSheetSkin(insertForm.Page);

                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes.Add("class", "row");
                HtmlGenericControl div1 = new HtmlGenericControl("div");
                div1.Attributes.Add("class", "col-4 col-sm-4 col-md-4 col-lg-4 col-xl-3");
                HtmlGenericControl div2 = new HtmlGenericControl("div");
                div2.Attributes.Add("class", "input-group input-group-lg");
                HtmlGenericControl div3 = new HtmlGenericControl("div");
                div3.Attributes.Add("class", "col-4 col-sm-4 col-md-4 col-lg-4 col-xl-3");
                HtmlGenericControl div4 = new HtmlGenericControl("div");
                div4.Attributes.Add("class", "input-group input-group-lg");
                HtmlGenericControl div5 = new HtmlGenericControl("div");
                div5.Attributes.Add("class", "col-4 col-sm-4 col-md-4 col-lg-4 col-xl-3");
                HtmlGenericControl div6 = new HtmlGenericControl("div");
                div6.Attributes.Add("class", "input-group input-group-lg");
                HtmlGenericControl div7 = new HtmlGenericControl("div");
                div7.Attributes.Add("class", "col-4 col-sm-4 col-md-4 col-lg-4 col-xl-3");

                div2.Controls.Add(banklb);
                div2.Controls.Add(dropD);
                div1.Controls.Add(div2);

                h.Controls.Add(div1);

                div6.Controls.Add(branchlb);
                div6.Controls.Add(textBoxBranch);
                div5.Controls.Add(div6);

                h.Controls.Add(div5);

                div4.Controls.Add(accountlb);
                div4.Controls.Add(textBoxAccount);
                div3.Controls.Add(div4);

                h.Controls.Add(div3);

                div7.Controls.Add(btn);

                h.Controls.Add(div7);

                div.Controls.Add(h);
                insertForm.Controls.Add(div);

               

            }
            if (clearInputCheck)
            {
                Clear_Input();
                clearInputCheck = true;
            }
           
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            errorAccountLabel.Visible = false;
            errorBranchLabel.Visible = false;
            errorExistingUser.Visible = false;

            if (string.IsNullOrWhiteSpace(account.Text) || string.IsNullOrEmpty(account.Text))
            {
                errorAccountLabel.Visible = true;
                errorAccountLabel.Text = "אנא הכנס/י מספר חשבון";
                clearInputCheck = false;

            }
            else if (string.IsNullOrWhiteSpace(branch.Text) || string.IsNullOrEmpty(branch.Text))
            {
                errorBranchLabel.Visible = true;
                errorBranchLabel.Text = "אנא הכנס/י מספר סניף";
                clearInputCheck = false;
            }
            else if (!(Is_Numeric(account.Text)))
            {
                errorAccountLabel.Visible = true;
                errorAccountLabel.Text = "אנא הכנס/י רק מספרים";
            }
            else if (!(Is_Numeric(branch.Text)))
            {
                errorBranchLabel.Visible = true;
                errorBranchLabel.Text = "אנא הכנס/י רק מספרים";
            }
            else if (Ckeck_Input())
            {
                errorExistingUser.Visible = true;
                errorExistingUser.Text = "מספר חשבון קיים במערכת";
            }
            else
            {
                Enter_Values_To_db();

            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            Delete_Values_From_db(btn);
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            var userID = (string)Session["userID"];
           
            Session["userID"] = userID;
            Response.Redirect("details.aspx");
        }

        protected void Next_Click(object sender, EventArgs e)
        {
            
        }

    }
}
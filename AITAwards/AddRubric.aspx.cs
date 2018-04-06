using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class AddRubric : System.Web.UI.Page
    {
        int lstCriteria;

        protected void Page_Load(object sender, EventArgs e)
        {
            alertControl.Visible = false;
            if(!IsPostBack)
            InitializePage();
        }

        private void InitializePage()
        {
            IAdminDatabase adminDB = new AdminDB();

            List<AITEvent> aitEvent = new List<AITEvent>();
            List<AITCategories> lstAitCategories = new List<AITCategories>();

            aitEvent = adminDB.GetListEvent();

            ddlEvent.Items.Clear();
            ddlCategory.Items.Clear();
            ddlCCriteria.Items.Clear();

            for (int i = 0; i < 10; i++)
            {
                ddlCCriteria.Items.Insert(i, new ListItem((i+1).ToString(), (i+1).ToString()));
            }

            for (int i = 0; i < aitEvent.Count; i++)
            {
                ddlEvent.Items.Insert(i, new ListItem(aitEvent[i].Name, aitEvent[i].EventID.ToString()));
            }

            lstAitCategories = new List<AITCategories>();
            lstAitCategories = adminDB.GetListCategoryByEventID(int.Parse(ddlEvent.SelectedValue));

            for (int i = 0; i < lstAitCategories.Count; i++)
            {
                ddlCategory.Items.Insert(i, new ListItem(lstAitCategories[i].Name, lstAitCategories[i].CategoryID.ToString()));
            }
        }

        protected void Menu_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            switch (linkButton.ID)
            {
                case "lbtnAddEvent":
                    Response.Redirect("AddEvent.aspx");
                    break;
                case "lbtnUpdateEvent":
                    Response.Redirect("UpdateEvent.aspx");
                    break;
                case "lbtnAddCategory":
                    Response.Redirect("AddCategory.aspx");
                    break;
                case "lbtnUpdateCategory":
                    Response.Redirect("UpdateCategory.aspx");
                    break;
                case "lbtnInviteJudge":
                    Response.Redirect("InvitationJudge.aspx");
                    break;
                default:
                    break;
            }
        }

        private void ShowAlert(string text, bool error)
        {
            if (error)
            {
                lbAlert.Text = text;
                alertControl.Visible = true;
                alertControl.Attributes.Remove("class");
                alertControl.Attributes.Add("class", "alert alert-danger");
            }
            else
            {
                lbAlert.Text = text;
                alertControl.Visible = true;
                alertControl.Attributes.Remove("class");
                alertControl.Attributes.Add("class", "alert alert-success");
            }
        }

        protected void btnSearchCategories_Click(object sender, EventArgs e)
        {
            IAdminDatabase adminDB = new AdminDB();

            List<AITCategories> lstAitCategories = new List<AITCategories>();
            lstAitCategories = adminDB.GetListCategoryByEventID(int.Parse(ddlEvent.SelectedValue));

            ddlCategory.Items.Clear();
            for (int i = 0; i < lstAitCategories.Count; i++)
            {
                ddlCategory.Items.Insert(i, new ListItem(lstAitCategories[i].Name, lstAitCategories[i].CategoryID.ToString()));
            }
        }

        protected void btnAddRubric_Click(object sender, EventArgs e)
        {
            RubricDetail rubricDetail = new RubricDetail();
            List<CriteriaDetail> lstCriteriaDetails = new List<CriteriaDetail>();
            List<LevelCriteria> lstLevelCriterias = new List<LevelCriteria>();
            CriteriaDetail criteriaDetail = new CriteriaDetail();
            LevelCriteria levelCriteria = new LevelCriteria();

            AddRubricDetail addRubricDetail = new AddRubricDetail();
            addRubricDetail = AppSession.GetAddRubricDetail();
            for (int i = 0; i < addRubricDetail.CountCriteria; i++)
            {
                DropDownList ddl = new DropDownList();
                TextBox txtBox = new TextBox();
                ddl = (DropDownList)criteriaControl.FindControl("ddl" + i);
                txtBox = (TextBox)criteriaControl.FindControl("txt" + i);

                criteriaDetail = new CriteriaDetail();
                criteriaDetail.CategoryID = addRubricDetail.CategoryID;
                criteriaDetail.Name = txtBox.Text;

                lstLevelCriterias = new List<LevelCriteria>();
                for (int j = 0; j < int.Parse(ddl.SelectedValue); j++)
                {
                    levelCriteria = new LevelCriteria();
                    levelCriteria.Description = j.ToString();
                    levelCriteria.ValueScore = (float)j;
                    lstLevelCriterias.Add(levelCriteria);
                }

                criteriaDetail.LevelCritieria = lstLevelCriterias;
                lstCriteriaDetails.Add(criteriaDetail);
                
            }

            rubricDetail.ListCriteriaDetail = lstCriteriaDetails;

        }

        protected void btnAddCriteria_Click(object sender, EventArgs e)
        {
            AddRubricDetail addRubricDetail = new AddRubricDetail();
            addRubricDetail.EventID = int.Parse(ddlEvent.SelectedValue);
            addRubricDetail.CategoryID = int.Parse(ddlCategory.SelectedValue);
            addRubricDetail.CountCriteria = int.Parse(ddlCCriteria.SelectedValue);
            criteriaControl.Controls.Clear();
            lstCriteria = ddlCCriteria.Items.Count;

            for (int i = 0; i < int.Parse(ddlCCriteria.SelectedValue); i++)
            {
                criteriaControl.Controls.Add(new LiteralControl("<div class='form-group'><label class='form-control-label'>"));
                criteriaControl.Controls.Add(new LiteralControl("Criteria" + "" + "</label>"));

                criteriaControl.Controls.Add(new LiteralControl("Criteria " + (i+1) + "</label>&nbsp&nbsp"));
                TextBox textBox = new TextBox();
                textBox.ID = "txt" + i;
                textBox.CssClass = "form-control";
                criteriaControl.Controls.Add(textBox);

                criteriaControl.Controls.Add(new LiteralControl("<label class='form-control-label'>Rating </label>&nbsp&nbsp"));
                DropDownList ddList = new DropDownList();
                ddList.ID = "ddl" + i;
                ddList.CssClass = "btn btn-secondary dropdown-toggle";
                for (int j = 0; j < 10; j++)
                {
                    ddList.Items.Insert(j, new ListItem((j + 1).ToString(), (j + 1).ToString()));
                }
                criteriaControl.Controls.Add(ddList);
                criteriaControl.Controls.Add(new LiteralControl("</div>"));
            }



        }
    }
}
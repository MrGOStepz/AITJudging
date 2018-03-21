using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITAwards
{
    public partial class QuestionControl : System.Web.UI.UserControl
    {
        public Control RubricControl = new Control();
        public List<int> ListBtnRadioID;

        private RubricDetail _rubricDetail;
        private int _questionNo;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            _rubricDetail = AppSession.GetRubric();
            _questionNo = AppSession.GetQuestionNo();
            ListBtnRadioID = new List<int>();
            
            InitializeControl();
        }

        protected void InitializeControl()
        {
            RadioButton radioButton;
            StringBuilder strB = new StringBuilder();
            rubricTB.Controls.Clear();
            strB.Append("<th scope='col'>");
            strB.Append("#");
            strB.Append("</th>");

            for (int i = 0; i < _rubricDetail.ListCriteriaDetail[_questionNo].LevelCritieria.Count; i++)
            {
                strB.Append("<th scope='col'>");
                strB.Append(_rubricDetail.ListCriteriaDetail[_questionNo].LevelCritieria[i].ValueScore.ToString("0.00"));
                strB.Append("</th>");
            }

            lbHeader.Text = strB.ToString();

            rubricTB.Controls.Add(new LiteralControl("<tr>"));
            rubricTB.Controls.Add(new LiteralControl("<th scope='row'>"));
            rubricTB.Controls.Add(new LiteralControl(_rubricDetail.ListCriteriaDetail[_questionNo].Name));
            rubricTB.Controls.Add(new LiteralControl("</th>"));


            for (int i = 0; i < _rubricDetail.ListCriteriaDetail[_questionNo].LevelCritieria.Count; i++)
            {
                rubricTB.Controls.Add(new LiteralControl("<td>"));
                rubricTB.Controls.Add(new LiteralControl(_rubricDetail.ListCriteriaDetail[_questionNo].LevelCritieria[i].Description));
                rubricTB.Controls.Add(new LiteralControl("</td>"));
            }

            rubricTB.Controls.Add(new LiteralControl("</tr> <tr>"));
            rubricTB.Controls.Add(new LiteralControl("<th scope='row'>"));
            rubricTB.Controls.Add(new LiteralControl("Trick"));
            rubricTB.Controls.Add(new LiteralControl("</th>"));

            for (int i = 0; i < _rubricDetail.ListCriteriaDetail[_questionNo].LevelCritieria.Count; i++)
            {
                radioButton = new RadioButton();
                radioButton.ID = "btnRadio" + i;
                radioButton.GroupName = "levelGroup";
  
                ListBtnRadioID.Add(i);
                rubricTB.Controls.Add(new LiteralControl("<td>"));
                rubricTB.Controls.Add(radioButton);
                rubricTB.Controls.Add(new LiteralControl("</td>"));

            }

            rubricTB.Controls.Add(new LiteralControl("</tr>"));
            RubricControl = rubricTB;
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {          
            RadioButton radioButton = sender as RadioButton;

            int btnRadioID = int.Parse(radioButton.ID.Substring(8));

            for (int i = 0; i < ListBtnRadioID.Count; i++)
            {
                RadioButton radio = (RadioButton)rubricTB.FindControl("btnRadio" + ListBtnRadioID[i]);
                if (btnRadioID == i)
                    continue;
            }

        }


    }
}
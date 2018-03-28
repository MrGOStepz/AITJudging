﻿using System;
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
        public TextBox TextBoxDescription
        {
            get { return txtDescription; }
            set { txtDescription = value; }
        }

        public int TempQuestionON;
        public int projectID;

        private RubricDetail _rubricDetail;
        private int _questionNo;
        private int _radioButtonCheck = -1;
        private string _textBoxDescition = "";


        protected void Page_Load(object sender, EventArgs e)
        {

            _questionNo = AppSession.GetQuestionNo();
            _rubricDetail = AppSession.GetRubric();
            _questionNo = AppSession.GetQuestionNo();
            TempQuestionON = _questionNo;

            if (_questionNo > _rubricDetail.ListCriteriaDetail.Count)
                Response.Redirect("Result.aspx");



            ListBtnRadioID = new List<int>();

            InitializeControl();


        }

        protected void InitializeControl()
        {
            ProjectDetail projectDetail = new ProjectDetail();
            RubricDetail rubricDetail = new RubricDetail();

            List<CriteriaDetail> lstCriteriaDetail = new List<CriteriaDetail>();
            List<LevelCriteria> lstLevelCriteria = new List<LevelCriteria>();

            IJudgeDatabase judgeDatabase = new JudgeDB();
            projectDetail = judgeDatabase.GetProjectbyProjectID(projectID);

            imgProject.ImageUrl = "Images/Projects/" + projectDetail.PathFile;
            System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(imgProject.ImageUrl));

            if (image.Height > image.Width)
            {
                imgProject.Attributes.Add("style", "width: 50vh; height: auto; max-height:90vh;");
                lbSetCol.Text = "<div class='col text-center'>";
                lbCDiv.Text = "";
                lbCDiv2.Text = "</div>";
            }
            else
            {
                imgProject.Attributes.Add("style", "width: auto; height: 50vh;");
                lbSetCol.Text = "</div> <div class='row padding-10'>";
                lbCDiv.Text = "</div>";
                lbCDiv2.Text = "";
            }

            _radioButtonCheck = -1;
            _textBoxDescition = "";
            _questionNo = AppSession.GetQuestionNo();

            List<AnswerDetail> lstAnswerDetail = new List<AnswerDetail>();
            lstAnswerDetail = AppSession.GetListAnswer();

            if (AppSession.GetQuestionNo() > _rubricDetail.ListCriteriaDetail.Count - 1)
            {
                if (lstAnswerDetail == null || lstAnswerDetail.Count < _rubricDetail.ListCriteriaDetail.Count)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please fill up all answer!')", true);
                    AppSession.SetQuestionNo(AppSession.GetQuestionNo() - 1);
                    _questionNo = AppSession.GetQuestionNo();
                }
                else
                {
                    AppSession.SetQuestionNo(AppSession.GetQuestionNo() - 1);
                    //TODO Clear Session
                    Response.Redirect("Result.aspx");
                }

            }

            if (AppSession.GetQuestionNo() == _rubricDetail.ListCriteriaDetail.Count - 1)
                btnNext.Text = "Done";
            else
                btnNext.Text = "Next";

            if (lstAnswerDetail != null)
            {
                for (int i = 0; i < lstAnswerDetail.Count; i++)
                {
                    if (_questionNo == lstAnswerDetail[i].Question)
                    {
                        _radioButtonCheck = lstAnswerDetail[i].Answer;
                        _textBoxDescition = lstAnswerDetail[i].Description;
                    }
                }

            }
            RadioButton radioButton;
            StringBuilder strB = new StringBuilder();
            rubricTB.Controls.Clear();
            txtDescription.Text = "";
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
            rubricTB.Controls.Add(new LiteralControl(""));
            rubricTB.Controls.Add(new LiteralControl("</th>"));

            for (int i = 0; i < _rubricDetail.ListCriteriaDetail[_questionNo].LevelCritieria.Count; i++)
            {
                radioButton = new RadioButton();
                radioButton.ID = "btnRadio" + i;
                radioButton.GroupName = "levelGroup" + _questionNo;

                if (i == _radioButtonCheck)
                {
                    radioButton.Checked = true;
                    txtDescription.Text = _textBoxDescition;
                }

                ListBtnRadioID.Add(i);
                rubricTB.Controls.Add(new LiteralControl("<td>"));
                rubricTB.Controls.Add(radioButton);
                rubricTB.Controls.Add(new LiteralControl("</td>"));
            }

            rubricTB.Controls.Add(new LiteralControl("</tr>"));
            RubricControl = rubricTB;
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            SaveAnswerToSession();

            _questionNo++;
            AppSession.SetQuestionNo(AppSession.GetQuestionNo() + 1);

            InitializeControl();

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (AppSession.GetQuestionNo() == 0)
            {
                Response.Redirect("Rubric.aspx");
            }
            else
            {
                SaveAnswerToSession();
                _questionNo--;
                AppSession.SetQuestionNo(AppSession.GetQuestionNo() - 1);
                InitializeControl();
            }
        }

        protected void SaveAnswerToSession()
        {
            AnswerDetail answerDetail = new AnswerDetail();
            bool checkNewAnswer = false;

            for (int i = 0; i < _rubricDetail.ListCriteriaDetail[_questionNo].LevelCritieria.Count; i++)
            {
                RadioButton radioButton = (RadioButton)rubricTB.FindControl("btnRadio" + i);

                if (radioButton != null)
                {
                    if (radioButton.Checked == true)
                    {
                        answerDetail.Answer = i;
                        answerDetail.Description = txtDescription.Text;
                        answerDetail.Question = _questionNo;

                        List<AnswerDetail> lstAnswerDetail = new List<AnswerDetail>();
                        lstAnswerDetail = AppSession.GetListAnswer();

                        if (lstAnswerDetail != null)
                        {
                            for (int j = 0; j < lstAnswerDetail.Count; j++)
                            {
                                if (lstAnswerDetail[j].Question == AppSession.GetQuestionNo())
                                {
                                    lstAnswerDetail[j].Answer = answerDetail.Answer;
                                    lstAnswerDetail[j].Description = answerDetail.Description;
                                    lstAnswerDetail[j].Question = _questionNo;
                                    AppSession.SetListAnswer(lstAnswerDetail);
                                    checkNewAnswer = false;
                                    break;
                                }
                                else
                                {
                                    checkNewAnswer = true;
                                }
                            }

                            if (checkNewAnswer)
                            {
                                lstAnswerDetail.Add(answerDetail);
                                AppSession.SetListAnswer(lstAnswerDetail);
                            }
                        }
                        else
                        {
                            List<AnswerDetail> lstAnswer = new List<AnswerDetail>();
                            lstAnswer.Add(answerDetail);
                            AppSession.SetListAnswer(lstAnswer);
                        }
                    }
                }
            }

        }


    }
}

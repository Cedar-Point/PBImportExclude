using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.IO;

namespace PBExcludeList
{
    public partial class Default : System.Web.UI.Page
    {
        private PBExlusionList ExlusionList = new PBExlusionList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Session["PBExclusion"] == null)
            {
                if (File.Exists(Properties.Settings.Default.PBExclusionListXMLPath))
                {
                    FileStream file = File.OpenRead(Properties.Settings.Default.PBExclusionListXMLPath);
                    XDocument doc = XDocument.Load(file);
                    file.Close();
                    List<XElement> PNs = doc.Root.Descendants("PN").ToList();
                    foreach (XElement pn in PNs)
                    {
                        ExlusionList.Add(new PBExlusion(pn.Attribute("From").Value, pn.Attribute("To").Value, pn.Attribute("Desc").Value));
                    }
                }
                Page.Session["PBExclusion"] = ExlusionList;
            }
            else
            {
                ExlusionList = (PBExlusionList)Page.Session["PBExclusion"];
            }
            LoadExlusionList();
        }
        private void LoadExlusionList()
        {
            for (int c = 1; c < tblDataList.Rows.Count; c++)
            {
                tblDataList.Rows.RemoveAt(c);
            }
            foreach (PBExlusion exl in ExlusionList)
            {
                TableRow tr = new TableRow();
                int exlIndex = tblDataList.Rows.Add(tr);
                TableCell cbCell = new TableCell();
                Button button = new Button();
                button.Text = "Remove";
                button.Attributes.Add("exlIndex", (exlIndex - 1).ToString());
                button.Click += RemoveButton_Click;
                cbCell.Controls.Add(button);
                tr.Cells.Add(cbCell);
                tr.Cells.Add(new TableCell()
                {
                    Text = exl.StartRange.ToString()
                });
                tr.Cells.Add(new TableCell()
                {
                    Text = exl.EndRange.ToString()
                });
                tr.Cells.Add(new TableCell()
                {
                    Text = exl.Description
                });
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (!int.TryParse(btn.Attributes["exlIndex"], out int index)) return;
            ExlusionList.RemoveAt(index);
            Response.Redirect("./Default.aspx");
        }

        protected void btnAddSet_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbStartRange.Text, out _) && int.TryParse(tbEndRange.Text, out _) && tbDesc.Text != "")
            {
                ExlusionList.Add(new PBExlusion(tbStartRange.Text, tbEndRange.Text, tbDesc.Text));
                Response.Redirect("./Default.aspx");
            }
            else
            {
                lblAddError.Visible = true;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Page.Session["PBExclusion"] = null;
            Response.Redirect("./Default.aspx");
        }
        protected void btnSave0_Click(object sender, EventArgs e)
        {
            XDocument xdoc = new XDocument(new XElement("PBExlusions"));
            foreach(PBExlusion exl in ExlusionList)
            {
                XElement xelm = new XElement("PN");
                xelm.Add(new XAttribute("From", exl.StartRange));
                xelm.Add(new XAttribute("To", exl.EndRange));
                xelm.Add(new XAttribute("Desc", exl.Description));
                xdoc.Root.Add(xelm);
            }
            if (File.Exists(Properties.Settings.Default.PBExclusionListXMLPath)) File.Delete(Properties.Settings.Default.PBExclusionListXMLPath);
            FileStream stream = File.OpenWrite(Properties.Settings.Default.PBExclusionListXMLPath);
            xdoc.Save(stream);
            stream.Close();
        }
    }
    public class PBExlusionList : List<PBExlusion> { }
    public class PBExlusion
    {
        public PBExlusion() { }
        public PBExlusion(string StartRange, string EndRange, string Description)
        {
            this.StartRange = StartRange;
            this.EndRange = EndRange;
            this.Description = Description;
        }
        public string StartRange = "";
        public string EndRange = "";
        public string Description = "";
    }
}
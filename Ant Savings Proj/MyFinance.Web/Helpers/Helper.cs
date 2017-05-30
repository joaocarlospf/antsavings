using MyFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Web.Helpers
{
    public class Helper
    {
        private const char ENUM_SEPERATOR_CHARACTER = ',';
        public static string GetDescription(Enum value)
        {
            // Check for Enum that is marked with FlagAttribute         
            var entries = value.ToString().Split(ENUM_SEPERATOR_CHARACTER);
            var description = new string[entries.Length];
            for (var i = 0; i < entries.Length; i++)
            {
                var fieldInfo = value.GetType().GetField(entries[i].Trim());
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                description[i] = (attributes.Length > 0) ? attributes[0].Description : entries[i].Trim();
            }

            return String.Join(", ", description);
        }

        public static List<SelectListItem> GetDescriptions(Type enumType)
        {
            var selectList = new List<SelectListItem>();
            // get enum list
            var enums = Enum.GetValues(enumType);
            foreach (var e in enums)
            {
                // Check for Enum that is marked with FlagAttribute         
                var fieldInfo = e.GetType().GetField(e.ToString());
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = (attributes.Length > 0) ? attributes[0].Description : e.ToString();
                var sli = new SelectListItem();
                sli.Value = ((int)e).ToString();
                sli.Text = description;
                selectList.Add(sli);
            }

            return selectList;
        }

        public static List<SelectListItem> GetListItem(List<Fund> list, string noneName = null)
        {
            List<SelectListItem> ret = new List<SelectListItem>();
            if (noneName != null)
                ret.Add(new SelectListItem() { Text = noneName, Value = "-1" });
            ret.AddRange(list.Select(f => new SelectListItem() { Text = f.Name, Value = f.ID.ToString() }));
            return ret;
        }

        public static List<SelectListItem> GetListItem(List<DistributionRule> list)
        {
            List<SelectListItem> ret = new List<SelectListItem>();
            ret.Add(new SelectListItem() { Text = "NENHUM", Value = "-1" });
            ret.AddRange(list.Select(f => new SelectListItem() { Text = f.Name, Value = f.Id.ToString() }));
            return ret;
        }

        public static List<SelectListItem> GetListItem(List<Reserve> list, string noneName = null)
        {
            List<SelectListItem> ret = new List<SelectListItem>();
            if (noneName != null)
                ret.Add(new SelectListItem() { Text = noneName, Value = "-1" });
            ret.AddRange(list.Select(f => new SelectListItem() { Text = f.Name, Value = f.ID.ToString() }));
            return ret;
        }

        public static string RenderPartialViewToString(string viewName, object model, ControllerContext cc, ViewDataDictionary vd, TempDataDictionary td)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = cc.RouteData.GetRequiredString("action");

            vd.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(cc, viewName);
                var viewContext = new ViewContext(cc, viewResult.View, vd, td, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
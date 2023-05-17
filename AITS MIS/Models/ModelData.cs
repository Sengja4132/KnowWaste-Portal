﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Web.Framework.General;
using Web.Framework.Models;
using Web.Framework.Property;
using Web.Framework.Server;

namespace UCOnline.Models
{
    public class ModelData : ModelBase
    {
        public string CountryID { get; set; }

        public string Year { get; set; }

        public ModelData() : base("", "")
        {
        }

        public override string Show()
        {
            if (CountryID == "0")
                return "";

            var output = new StringBuilder();

            var server = new ServerBase("country");
            server.SelectFilter("id", CountryID);

            var data = server.SelectQuery();

            if (CountryID != "0" && data.Rows.Count != 0)
            {
                var country = data.Rows[0]["name"].ToString();

                output.AppendFormat("<h1 class='c-font-uppercase c-font-bold'>{0}</h1>", country);

                var population = new ModelPopulation();
                population.CountyID = CountryID;
                output.Append(population.Show());

                if (Year != "0")
                {
                    output.AppendFormat("<h3 class='c-font-uppercase c-font-bold'>{0}</h3>", "Waste Data");
                    output.AppendFormat("<h4>(Unit: Thousand T/Year)</h3>");

                    var waste = new ModelWasteStream();
                    waste.CountryID = CountryID;
                    waste.Year = Year;
                    output.Append(waste.Show());

                    //output.AppendFormat("<h3 class='c-font-uppercase c-font-bold'>{0}</h3>", "References");

                    //var reference = new ModelReferences();
                    //reference.RefIDs = waste.RefIDs;
                    //reference.CountyID = CountryID;
                    //output.Append(reference.Show());

                    output.AppendFormat("<h3 class='c-font-uppercase c-font-bold'>{0}</h3>", "Legal Framework and Policy");

                    var policy = new ModelCountryPolicy();
                    policy.CountyID = CountryID;
                    output.Append(policy.Show());
                }

                return output.ToString();
            }
            else
            {
                output.AppendFormat("<h3 class='c-font-uppercase c-font-bold'>{0}</h3>", "Waste Data");

                var waste = new ModelWasteStream();
                waste.CountryID = CountryID;
                output.Append(waste.Show());

                return output.ToString();
            }
        }
    }
}
namespace Sitecore.Support.XA.Feature.SiteMetadata
{
  using Sitecore.XA.Feature.SiteMetadata.Models.Robots;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  public class RobotsContentFormatter: Sitecore.XA.Feature.SiteMetadata.RobotsContentFormatter
  {
    public override void Parse(IEnumerable<string> lines)
    {
      List<SitemapUrl> siteMapUrls = (from s in (from s in lines
                                                 where s.StartsWith("Sitemap:", StringComparison.Ordinal)
                                                 select s.Substring("Sitemap:".Length) into s
                                                 select s.TrimStart(' ').TrimEnd(' ')).Distinct()
                                      select new SitemapUrl(s)).ToList();
      IEnumerable<string> enumerable = from s in lines
                                       where !s.StartsWith("Sitemap:", StringComparison.Ordinal)
                                       select s;
      List<RobotsRecord> list = new List<RobotsRecord>();
      RobotsRecord robotsRecord = new RobotsRecord();
      foreach (string item in enumerable)
      {
        if (!string.IsNullOrWhiteSpace(item))
        {
          if (item.StartsWith("User-agent:", StringComparison.Ordinal))
          {
            robotsRecord = new RobotsRecord(item);
            list.Add(robotsRecord);
          }
          else if (IsValidRule(item))
          {
            robotsRecord.AddRule(item);
          }
        }
      }
      Records = GetUnique(list).ToList();
      SiteMapUrls = siteMapUrls;
    }
  }
}
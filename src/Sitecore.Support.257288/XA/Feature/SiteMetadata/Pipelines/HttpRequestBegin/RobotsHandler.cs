namespace Sitecore.Support.XA.Feature.SiteMetadata.Pipelines.HttpRequestBegin
{
  using Microsoft.Extensions.DependencyInjection;
  using Sitecore.DependencyInjection;
  using Sitecore.Pipelines;
  using Sitecore.Pipelines.HttpRequest;
  using Sitecore.Sites;
  using Sitecore.Web;
  using Sitecore.XA.Feature.SiteMetadata;
  using Sitecore.XA.Feature.SiteMetadata.Pipelines.GetRobotsContent;
  using Sitecore.XA.Foundation.Abstractions;
  using Sitecore.XA.Foundation.Multisite.Extensions;
  using Sitecore.XA.Foundation.SitecoreExtensions.Utils;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Web;
  using System.Web.Caching;

  public class RobotsHandler : Sitecore.XA.Feature.SiteMetadata.Pipelines.HttpRequestBegin.RobotsHandler
  {
    protected override string GetRobotsContent(HttpRequestArgs args)
    {
      IList<SiteContext> matchingSites = GetMatchingSites(args);
      StringBuilder sb = new StringBuilder();
      matchingSites.ToList().ForEach(delegate (SiteContext site)
      {
        sb.AppendLine(GetRobotsContent(site).ToString());
      });
      Sitecore.Support.XA.Feature.SiteMetadata.RobotsContentFormatter robotsContentFormatter = new Sitecore.Support.XA.Feature.SiteMetadata.RobotsContentFormatter();
      robotsContentFormatter.Parse(sb.ToString(), null);
      return robotsContentFormatter.RenderOutput();
    }
  }
}
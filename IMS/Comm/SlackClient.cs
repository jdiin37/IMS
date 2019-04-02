using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace IMS.Comm
{
  public class SlackClient
  {
    private readonly Uri slackUri = null;

    public SlackClient()
    {
      string slackWebHookUrl = "https://hooks.slack.com/services/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";
      slackUri = new Uri(slackWebHookUrl);
    }
    public void PostMessage(string msg)
    {
      var postData = JsonConvert.SerializeObject(new
      {

        icon_emoji = ":ghost:",
        username = "Bot",
        attachments = new[]
                   {
                            new {
                                text = msg,
                                color = "#36a64f",
                                mrkdwn_in = new []{"text"}
                            }
                        }
      });
      this.PostSlack(postData);
    }
    private void PostSlack(string postData)
    {
      using (var wc = new WebClient())
      {
        wc.Headers.Add(HttpRequestHeader.ContentType, "application/json;charset=UTF-8");
        wc.Encoding = Encoding.UTF8;
        wc.UploadString(slackUri, postData);
      }
    }
  }
}
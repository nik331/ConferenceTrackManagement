using System;
using System.Text.RegularExpressions;

namespace ThoughtWorks.CTM
{
    public class Talk
    {
        public string _topic;
        public TalkDuration _duration;
        public Talk(string _topicTitle)
        {
            try
            {
                string tempTopic = "";
                int tempDuration = -1;
                FetchTalkDetails(_topicTitle, out tempTopic, out tempDuration);//arrange the Talks based on different values provided
                TalkDuration _duration = new TalkDuration(tempDuration);
                this._duration = _duration;
                if (IsTitleInvalid(tempTopic))
                {
                    throw new Exception("Title Cannot contain Numeric values");
                }
                _topic = tempTopic;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void FetchTalkDetails(string topicTitle, out string topic, out int duration){
            topic = "";
            duration = 0;
            
            string tempDuration = Regex.Match(topicTitle, @"\d+").Value;
            if(tempDuration != ""){
                string tempNumber = Regex.Match(topicTitle.Replace(tempDuration, ""), @"\d+").Value;
                if(tempNumber != "")
                    throw new Exception("Title Cannot contain two Numeric values");
                if(tempDuration.Length > 2)
                    throw new Exception("Invalid Talk Duration");
                topic = topicTitle.Replace(tempDuration,"").Replace("min","").Replace("MIN","").Replace("Min","").Replace("Programg","Programming");
                duration = int.Parse(tempDuration);
                return;
            }
            else{
                if((topicTitle.ToLower().Contains("lightning"))||(topicTitle.ToUpper().Contains("LIGHTNING")))
                topic = topicTitle;
                duration = 5;
                return;
            }
        }
        private bool IsTitleInvalid(string title)
        {
            return Regex.IsMatch(title, @"[0-9]+$");
        }
    }
}

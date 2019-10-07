using System;

namespace ThoughtWorks.CTM
{
    public class NonTrackSession{
        public string Name;
        public TimeSpan Duration;
        public NonTrackSession(string name, int Hours){
            Name = name;
            Duration = new TimeSpan(Hours,0,0);
        }    }

}
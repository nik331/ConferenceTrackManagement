using System;

namespace ThoughtWorks.CTM
{
    public class TalkDuration
    {
        public int _value;
        public TalkDuration(int duration)
        {
            try
            {
                if (IsDurationInvalid(duration))
                    throw new Exception("Invalid Talk Duration");
                _value = duration;
            }
            catch (Exception frog)
            {
                throw frog;
            }

        }
        private bool IsDurationInvalid(int _duration)//check if duration of Talk is valid as per rules i.e. < 60 mins
        {
            if((_duration < 0)||(_duration > 60))
                return true;
            return false;
        }
    }

}

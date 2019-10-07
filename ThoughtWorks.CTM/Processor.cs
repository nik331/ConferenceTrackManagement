using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThoughtWorks.CTM
{
    public class Processor
    {
        /// <summary>
        /// To process the input.
        /// </summary>
        /// <param name="lines">An input</param>
        /// <param name="numberOfTracks">No of tracks</param>
        /// <returns>List of conference track</returns>
        public List<ConferenceTrack> ProcessFile(string[] lines, int numberOfTracks)
        {
            if(numberOfTracks <= 0)
            {
                throw new Exception("Number of tracks can not be negative or zero");
            }

            List<Talk> talkList = CreateTalkList(lines); 
            List<ConferenceTrack> _conferenceTrack = CreateTracks(numberOfTracks);
            Scheduler(talkList, _conferenceTrack);
            return _conferenceTrack;
        }

        /// <summary>
        /// To create a program
        /// </summary>
        /// <param name="conferenceTracks">List of conference tracks</param>
        /// <returns>An output</returns>
        public string CreateProgram(List<ConferenceTrack> conferenceTracks)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Conference Track Program:");

            foreach (ConferenceTrack CT in conferenceTracks)
            {
                var currentTime = new TimeSpan(9, 0, 0);

                stringBuilder.AppendLine("Track " + CT._trackNumber.ToString() + ":");

                //calculate the time ->//morning
                TimeSpan resultTimeMorning = TimeSpan.FromHours(9);
                resultTimeMorning = TimeSpan.FromMinutes(resultTimeMorning.TotalMinutes);
                string fromTimeStringM = resultTimeMorning.ToString("hh':'mm");
                //evening
                TimeSpan resultTimeEvening = TimeSpan.FromHours(1);
                resultTimeEvening = TimeSpan.FromMinutes(resultTimeEvening.TotalMinutes);
                string fromTimeStringE = resultTimeEvening.ToString("hh':'mm");
                //<- time calculation ends here

                for (int i = 0; i < CT._morningSession.SessionTalks.Count; i++)
                {
                    fromTimeStringM = resultTimeMorning.ToString("hh':'mm");
                    stringBuilder.AppendLine(fromTimeStringM + "AM " + CT._morningSession.SessionTalks[i]._topic);

                    int time = CT._morningSession.SessionTalks[i]._duration._value;
                    resultTimeMorning = TimeSpan.FromMinutes(resultTimeMorning.TotalMinutes + time);
                }

                stringBuilder.AppendLine("12:00PM Lunch");

                for (int i = 0; i < CT._eveningSession.SessionTalks.Count; i++)
                {
                    fromTimeStringE = resultTimeEvening.ToString("hh':'mm");
                    stringBuilder.AppendLine(fromTimeStringE + "PM " + CT._eveningSession.SessionTalks[i]._topic);

                    int time = CT._eveningSession.SessionTalks[i]._duration._value;
                    resultTimeEvening = TimeSpan.FromMinutes(resultTimeEvening.TotalMinutes + time);

                }

                //networking event
                if (resultTimeEvening < TimeSpan.FromHours(4))
                    resultTimeEvening = TimeSpan.FromHours(4);
                if (resultTimeEvening > TimeSpan.FromHours(5))
                    resultTimeEvening = TimeSpan.FromHours(5);
                fromTimeStringE = resultTimeEvening.ToString("hh':'mm");
                stringBuilder.AppendLine(fromTimeStringE + "PM Networking Event");
            }
            return stringBuilder.ToString();
        }


        private List<Talk> CreateTalkList(string[] lines)
        {
            List<Talk> SelectedTalks = new List<Talk>();
            foreach (string line in lines)
            {
                Talk newTalk = new Talk(line);
                SelectedTalks.Add(newTalk);
            }
            List<Talk> SortedTalkList = SelectedTalks.OrderBy(o => o._duration._value).ToList();
            return SortedTalkList;
        }
               
        private List<ConferenceTrack> CreateTracks(int NumberOfTracks)
        {
            List<ConferenceTrack> CT = new List<ConferenceTrack>();
            ConferenceTrack TrackProgram;
            for (int i = 0; i < NumberOfTracks; i++)
            {
                TrackProgram = new ConferenceTrack(i + 1);
                CT.Add(TrackProgram);
            }
            return CT;
        }

        private void Scheduler(List<Talk> talkList, List<ConferenceTrack> conferenceTrack)
        {
            foreach (ConferenceTrack CT in conferenceTrack)
            {
                bool MorningSessionFull = false;
                bool EveningSessionFull = false;

                //morning session
                TimeSpan morningTS = CT._morningSession.Duration;
                double tempTime = morningTS.TotalMinutes;
                for (int i = talkList.Count - 1; i >= 0; i--)
                {
                    //for morning Session
                    if ((tempTime >= double.Parse(talkList[i]._duration._value.ToString())) && (!MorningSessionFull))
                    {
                        CT._morningSession.SessionTalks.Add(talkList[i]);
                        tempTime = tempTime - double.Parse(talkList[i]._duration._value.ToString());
                        talkList.RemoveAt(i);
                        if (tempTime == 0)
                        {
                            MorningSessionFull = true;
                        }
                    }
                }

                CT._timeSaved += int.Parse(tempTime.ToString()); //total time saved in the morning session only

                //evening session
                TimeSpan eveningTS = CT._eveningSession.Duration;
                tempTime = eveningTS.TotalMinutes;
                for (int i = talkList.Count - 1; i >= 0; i--)
                {
                    //for evening session
                    if (MorningSessionFull)
                    {
                        if ((tempTime >= double.Parse(talkList[i]._duration._value.ToString())) && (!EveningSessionFull))
                        {
                            CT._eveningSession.SessionTalks.Add(talkList[i]);
                            tempTime = tempTime - double.Parse(talkList[i]._duration._value.ToString());
                            talkList.RemoveAt(i);
                            if (tempTime == 0)
                            {
                                EveningSessionFull = true;
                            }
                        }
                    }
                }

                CT._timeSaved += int.Parse(tempTime.ToString());
            }
        }
    }
}

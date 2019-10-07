namespace ThoughtWorks.CTM
{
    public class ConferenceTrack
    {
        public int _trackNumber;
        public TrackSession _morningSession;
        public NonTrackSession _lunch;
        public TrackSession _eveningSession;
        public NonTrackSession _networkingEvent;
        public int _timeSaved;

        public ConferenceTrack(int trackNumber)
        {
            _trackNumber = trackNumber;
            _morningSession = new TrackSession("MorningSession", 3);
            _lunch = new NonTrackSession("Lunch", 1);
            _eveningSession = new TrackSession("EveningSession", 4);
            _networkingEvent = new NonTrackSession("NetworkingEvent", 1);
            _timeSaved = 0;
        }
    }
}
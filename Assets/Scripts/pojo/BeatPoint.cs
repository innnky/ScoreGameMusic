namespace pojo
{
    public class BeatPoint
    {
        private int beat;
        private float BPM;
        private int time;
        private float offset;
        private int bar;
        private int beatPerBar;
        private int beatInBar;
        private float barStartOffset;
        private float _relativeStart;
        private float length;
        public BeatPoint(int beat, float BPM, float offset, int beatPerBar, float length)
        {
            this.beat = beat;
            this.BPM = BPM;
            this.time = (int)(60000 * beat / BPM);;
            this.offset = offset;
            this.beatPerBar = beatPerBar;
            this.bar = beat / beatPerBar;
            this.beatInBar = beat % beatPerBar;
            this.length = length;
        }

        public int Beat
        {
            get => beat;
            set => beat = value;
        }

        public float Bpm
        {
            get => BPM;
            set => BPM = value;
        }

        public int Time
        {
            get => time;
            set => time = value;
        }

        public float Offset
        {
            get => offset;
            set => offset = value;
        }

        public int Bar
        {
            get => bar;
            set => bar = value;
        }

        public int BeatPerBar
        {
            get => beatPerBar;
            set => beatPerBar = value;
        }

        public int BeatInBar
        {
            get => beatInBar;
            set => beatInBar = value;
        }

        public float BarStartOffset
        {
            get => barStartOffset;
            set => barStartOffset = value;
        }

        public float RelativeStart
        {
            get => _relativeStart;
            set => _relativeStart = value;
        }

        public float Length
        {
            get => length;
            set => length = value;
        }
    }
}
namespace Logic
{
    public interface ICounter
    {
        public int Counter { get; set; }
        public void IncreaseCounter(int value);
        public void UpdateText();
    }
}
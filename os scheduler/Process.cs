public class Process
{
    public int arrival { get; set; }
    public int burst { get; set; }
    public int priority { get; set; }
    public int id { get; set; }
    public int start { get; set; }
    public int end { get; set; }

    public Process(int id, int arrival, int burst)
    { this.id = id; this.arrival = arrival; this.burst = burst; }

    public Process(int id, int arrival, int burst, int priority)
    { this.id = id; this.arrival = arrival; this.burst = burst; this.priority = priority; }

    public Process(int id, int arrival, int burst, int priority, int start, int end)
    { this.id = id; this.arrival = arrival; this.burst = burst; this.priority = priority; this.start = start; this.end = end; }

    public Process(Process p)
    {
        arrival = p.arrival; burst = p.burst; priority = p.priority; id = p.id; start = p.start; end = p.end;
    }
}
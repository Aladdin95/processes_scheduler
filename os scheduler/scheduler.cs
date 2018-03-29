using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace os_scheduler
{
    public class scheduler
    {
        
        public string method;
        public int nprocess;
        public List<Process> input ; //new List<Process>(nprocess);
        public List<Process> output ;// new List<Process>(nprocess);
        public int start;
        public int end;

        public scheduler()
        {
            nprocess = 0;
            method = "";
            
        }

        public void fcfs_sort(ref List<Process> t)
        {
            t.Sort(delegate(Process x, Process y)
            {
                if (x.arrival == y.arrival) return 0;
                else if (x.arrival < y.arrival) return 1;
                else return -1;
            });
        }

        public void fcfs()
        {
            fcfs_sort(ref input);
            input.Reverse();
            output.Add(new Process(input[0].id, input[0].arrival, input[0].burst, 
                0, input[0].arrival, input[0].arrival + input[0].burst));

            for (int i = 1; i < nprocess; ++i)
            {
                if (input[i].arrival < input[i - 1].arrival + input[i - 1].burst)
                {
                    output.Add(new Process(input[i].id, input[i].arrival , input[i].burst,
                        0 , input[i - 1].arrival + input[i - 1].burst ,
                        input[i - 1].arrival + input[i - 1].burst + input[i].burst
                        ));
                }
                else
                {
                    output.Add(new Process(input[i].id, input[i].arrival, input[i].burst,
                        0, input[i].arrival, input[i].arrival+input[i].burst
                        ));
                }
            }
        }

        public void sjf_non_preemptive()
        {
            List<Process> temp = new List<Process>(input);
            List<Process> subtemp = new List<Process>(nprocess);

            sjf_sort(ref temp);

            start = temp[0].arrival;
            end = temp[0].arrival + temp[0].burst;

            output.Add(new Process(temp[0].id, temp[0].arrival, temp[0].burst, temp[0].priority, start, end));

            for (int j = 1; j < nprocess; j++)
            {
                if (temp[j].arrival > end)
                {
                    if (subtemp.Count() > 0)
                    {
                        subtemp.Sort(delegate(Process x, Process y)
                        {
                            if (x.burst == y.burst) return 0;
                            else if (x.burst < y.burst) return 1;
                            else return -1;
                        });
                        start = end;
                        end += subtemp.Last().burst;
                        output.Add(new Process(subtemp.Last().id, subtemp.Last().arrival, subtemp.Last().burst, subtemp.Last().priority, start, end));
                        subtemp.RemoveAt(subtemp.Count - 1);
                        j--;
                    }
                    else
                    {
                        start = temp[j].arrival;
                        end = temp[j].arrival + temp[j].burst;
                        output.Add(new Process(temp[j].id, temp[j].arrival, temp[j].burst, temp[j].priority, start, end));
                    }
                }
                else
                {
                    subtemp.Add(temp[j]);
                }
            }

            subtemp.Sort(delegate(Process x, Process y)
            {
                if (x.burst == y.burst) return 0;
                else if (x.burst < y.burst) return 1;
                else return -1;
            });

            while (subtemp.Count() > 0)
            {
                start = end;
                end += subtemp.Last().burst;
                output.Add(new Process(subtemp.Last().id, subtemp.Last().arrival, subtemp.Last().burst, subtemp.Last().priority, start, end));
                subtemp.RemoveAt(subtemp.Count - 1);
            }
        }

        public void sjf_sort(ref List<Process> t)
        {
            t.Sort(
            delegate(Process x, Process y)
            {
                if (x.arrival == y.arrival)
                {
                    if (x.burst == y.burst) return 0;
                    else if (x.burst < y.burst) return 1;
                    else return -1;
                }
                else if (x.arrival < y.arrival) return 1;
                else return -1;
            }
            );
        }

        public void priority_sort(ref List<Process> t)
        {
            t.Sort(
            delegate(Process x, Process y)
            {
                if (x.arrival == y.arrival)
                {
                    if (x.priority == y.priority) return 0;
                    else if (x.priority < y.priority) return 1;
                    else return -1;
                }
                else if (x.arrival < y.arrival) return 1;
                else return -1;
            }
            );
        }

        public void sjf_preemptive()
        {
            List<Process> temp = new List<Process>(input);
            List<Process> subtemp = new List<Process>(nprocess);

            sjf_sort(ref temp);

            start = temp.Last().arrival;
            end = temp.Last().arrival + temp.Last().burst;

            output.Add(new Process(temp.Last().id, temp.Last().arrival, temp.Last().burst, temp.Last().priority, start, end));

            temp.RemoveAt(temp.Count - 1);

            while (temp.Count() > 0)
            {
                if (temp.Last().arrival <= end)
                {
                    if ((temp.Last().arrival + temp.Last().burst) < end)
                    {

                        subtemp.Add(new Process(output.Last()));

                        subtemp.Last().burst -= temp.Last().arrival - subtemp.Last().start;

                        output.Last().burst = temp.Last().arrival - output.Last().start;
                        output.Last().end = temp.Last().arrival;

                        start = temp.Last().arrival;
                        end = temp.Last().arrival + temp.Last().burst;
                        output.Add(new Process(temp.Last().id, temp.Last().arrival, temp.Last().burst, temp.Last().priority, start, end));
                        temp.RemoveAt(temp.Count - 1);
                    }
                    else
                    {
                        subtemp.Add(temp.Last());
                        temp.RemoveAt(temp.Count - 1);
                    }
                }

                else
                {
                    if (subtemp.Count() > 0)
                    {
                        subtemp.Sort(delegate(Process x, Process y)
                        {
                            if (x.burst == y.burst) return 0;
                            else if (x.burst < y.burst) return 1;
                            else return -1;
                        });

                        start = end;
                        end += subtemp.Last().burst;
                        output.Add(new Process(subtemp.Last().id, subtemp.Last().arrival, subtemp.Last().burst, subtemp.Last().priority, start, end));
                        subtemp.RemoveAt(subtemp.Count - 1);
                    }
                    else
                    {
                        start = temp.Last().arrival;
                        end = temp.Last().arrival + temp.Last().burst;
                        output.Add(new Process(temp.Last().id, temp.Last().arrival, temp.Last().burst, temp.Last().priority, start, end));
                    }
                }
            }

            subtemp.Sort(delegate(Process x, Process y)
            {
                if (x.burst == y.burst) return 0;
                else if (x.burst < y.burst) return 1;
                else return -1;
            });
            while (subtemp.Count() > 0)
            {
                start = end;
                end += subtemp.Last().burst;
                output.Add(new Process(subtemp.Last().id, subtemp.Last().arrival, subtemp.Last().burst, subtemp.Last().priority, start, end));
                subtemp.RemoveAt(subtemp.Count - 1);
            }
        }

        public void priority_preemptive()
        {
            List<Process> temp = new List<Process>(input);
            List<Process> subtemp = new List<Process>(nprocess);

            priority_sort(ref temp);

            start = temp.Last().arrival;
            end = temp.Last().arrival + temp.Last().burst;

            output.Add(new Process(temp.Last().id, temp.Last().arrival, temp.Last().burst, temp.Last().priority, start, end));

            temp.RemoveAt(temp.Count - 1);

            while (temp.Count() > 0)
            {
                if (temp.Last().arrival <= end)
                {
                    if (temp.Last().priority < output.Last().priority)
                    {

                        subtemp.Add(new Process(output.Last()));

                        subtemp.Last().burst -= temp.Last().arrival - subtemp.Last().start;

                        output.Last().burst = temp.Last().arrival - output.Last().start;
                        output.Last().end = temp.Last().arrival;

                        start = temp.Last().arrival;
                        end = temp.Last().arrival + temp.Last().burst;
                        output.Add(new Process(temp.Last().id, temp.Last().arrival, temp.Last().burst, temp.Last().priority, start, end));
                        temp.RemoveAt(temp.Count - 1);
                    }
                    else
                    {
                        subtemp.Add(temp.Last());
                        temp.RemoveAt(temp.Count - 1);
                    }
                }

                else
                {
                    if (subtemp.Count() > 0)
                    {
                        subtemp.Sort(delegate(Process x, Process y)
                        {
                            if (x.priority == y.priority) return 0;
                            else if (x.priority < y.priority) return 1;
                            else return -1;
                        });

                        start = end;
                        end += subtemp.Last().burst;
                        output.Add(new Process(subtemp.Last().id, subtemp.Last().arrival, subtemp.Last().burst, subtemp.Last().priority, start, end));
                        subtemp.RemoveAt(subtemp.Count - 1);
                    }
                    else
                    {
                        start = temp.Last().arrival;
                        end = temp.Last().arrival + temp.Last().burst;
                        output.Add(new Process(temp.Last().id, temp.Last().arrival, temp.Last().burst, temp.Last().priority, start, end));
                    }
                }
            }

            subtemp.Sort(delegate(Process x, Process y)
            {
                if (x.priority == y.priority) return 0;
                else if (x.priority < y.priority) return 1;
                else return -1;
            });
            while (subtemp.Count() > 0)
            {
                start = end;
                end += subtemp.Last().burst;
                output.Add(new Process(subtemp.Last().id, subtemp.Last().arrival, subtemp.Last().burst, subtemp.Last().priority, start, end));
                subtemp.RemoveAt(subtemp.Count - 1);
            }
        }

        public void priority_non_preemptive()
        {
            List<Process> temp = new List<Process>(input);
            List<Process> subtemp = new List<Process>(nprocess);

            priority_sort(ref temp);
            temp.Reverse();

            start = temp[0].arrival;
            end = temp[0].arrival + temp[0].burst;

            output.Add(new Process(temp[0].id, temp[0].arrival, temp[0].burst, temp[0].priority, start, end));

            for (int j = 1; j < nprocess; j++)
            {
                if (temp[j].arrival > end)
                {
                    if (subtemp.Count() > 0)
                    {
                        subtemp.Sort(delegate(Process x, Process y)
                        {
                            if (x.priority == y.priority) return 0;
                            else if (x.priority < y.priority) return 1;
                            else return -1;
                        });
                        start = end;
                        end += subtemp.Last().burst;
                        output.Add(new Process(subtemp.Last().id, subtemp.Last().arrival, subtemp.Last().burst, subtemp.Last().priority, start, end));
                        subtemp.RemoveAt(subtemp.Count - 1);
                        j--;
                    }
                    else
                    {
                        start = temp[j].arrival;
                        end = temp[j].arrival + temp[j].burst;
                        output.Add(new Process(temp[j].id, temp[j].arrival, temp[j].burst, temp[j].priority, start, end));
                    }
                }
                else
                {
                    subtemp.Add(temp[j]);
                }
            }

            subtemp.Sort(delegate(Process x, Process y)
            {
                if (x.priority == y.priority) return 0;
                else if (x.priority < y.priority) return 1;
                else return -1;
            });

            while (subtemp.Count() > 0)
            {
                start = end;
                end += subtemp.Last().burst;
                output.Add(new Process(subtemp.Last().id, subtemp.Last().arrival, subtemp.Last().burst, subtemp.Last().priority, start, end));
                subtemp.RemoveAt(subtemp.Count - 1);
            }
        }

    }
}

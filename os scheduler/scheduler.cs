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
        public List<Process> output_with_idle;
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
                if (x.arrival > y.arrival) return -1;
                else return 1;
            });
        }

        public void fcfs()
        {
            fcfs_sort(ref input);

            List<Process> temp = new List<Process>(input);

            start = temp.Last().arrival;
            end = start + temp.Last().burst;
            output.Add(new Process(temp.Last().id, temp.Last().arrival, temp.Last().burst, temp.Last().priority, start, end));
            temp.RemoveAt(temp.Count - 1);

            while (temp.Count > 0)
            {
                if (temp.Last().arrival < end)
                {
                    start = end;
                    end += temp.Last().burst;
                }
                else
                {
                    start = temp.Last().arrival;
                    end = start + temp.Last().burst;
                }
                output.Add(new Process(temp.Last().id, temp.Last().arrival, temp.Last().burst, temp.Last().priority, start, end));
                temp.RemoveAt(temp.Count - 1);
            }
        }

        public void sjf_non_preemptive()
        {
            List<Process> temp = new List<Process>(input);
            List<Process> subtemp = new List<Process>(nprocess);

            sjf_sort(ref temp);

            start = temp.Last().arrival;
            end = temp.Last().arrival + temp.Last().burst;

            output.Add(new Process(temp.Last().id, temp.Last().arrival, temp.Last().burst, temp.Last().priority, start, end));
            temp.RemoveAt(temp.Count - 1);

            while (temp.Count > 0)
            {
                if (temp.Last().arrival > end)
                {
                    if (subtemp.Count() > 0)
                    {
                        subtemp.Sort(delegate(Process x, Process y)
                        {
                            if (x.burst > y.burst) return -1;
                            else return 1;
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
                        temp.RemoveAt(temp.Count - 1);
                    }
                }
                else
                {
                    subtemp.Add(temp.Last());
                    temp.RemoveAt(temp.Count - 1);
                }
            }

            subtemp.Sort(delegate(Process x, Process y)
            {
                if (x.burst > y.burst) return -1;
                else return 1;
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
                    if (x.burst > y.burst) return -1;
                    else return 1;
                }
                else if (x.arrival > y.arrival) return -1;
                else return 1;
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
                    if (x.priority > y.priority) return -1;
                    else return 1;
                }
                else if (x.arrival > y.arrival) return -1;
                else return 1;
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
                            if (x.burst > y.burst) return -1;
                            else return 1;
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
                        temp.RemoveAt(temp.Count-1);
                    }
                }
            }

            subtemp.Sort(delegate(Process x, Process y)
            {
                if (x.burst > y.burst) return -1;
                else return 1;
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
                            if (x.priority > y.priority) return -1;
                            else return 1;
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
                        temp.RemoveAt(temp.Count - 1);
                    }
                }
            }

            subtemp.Sort(delegate(Process x, Process y)
            {
                if (x.priority > y.priority) return -1;
                else return 1;
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

            start = temp.Last().arrival;
            end = temp.Last().arrival + temp.Last().burst;

            output.Add(new Process(temp.Last().id, temp.Last().arrival, temp.Last().burst, temp.Last().priority, start, end));

            temp.RemoveAt(temp.Count - 1);

            while (temp.Count > 0)
            {
                if (temp.Last().arrival > end)
                {
                    if (subtemp.Count() > 0)
                    {
                        subtemp.Sort(delegate(Process x, Process y)
                        {
                            if (x.priority > y.priority) return -1;
                            else return 1;
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
                        temp.RemoveAt(temp.Count - 1);
                    }
                }
                else
                {
                    subtemp.Add(temp.Last());
                    temp.RemoveAt(temp.Count - 1);
                }
            }

            subtemp.Sort(delegate(Process x, Process y)
            {
                if (x.priority > y.priority) return -1;
                else return 1;
            });

            while (subtemp.Count() > 0)
            {
                start = end;
                end += subtemp.Last().burst;
                output.Add(new Process(subtemp.Last().id, subtemp.Last().arrival, subtemp.Last().burst, subtemp.Last().priority, start, end));
                subtemp.RemoveAt(subtemp.Count - 1);
            }
        }

        public int min(int x, int y) { return (x > y) ? y : x; }

        public void RR(int quantum)
        {
            List<Process> temp = new List<Process>(input);
            List<Process> subtemp = new List<Process>(nprocess + 1);
            for (int i = 0; i <= nprocess; ++i)
            {
                subtemp.Add(new Process(0, 0, 0));
            }

            int front = 0, rear = 0;

            fcfs_sort(ref temp);

            start = temp.Last().arrival;
            end = start + temp.Last().burst;
            output.Add(new Process(temp.Last().id, temp.Last().arrival, temp.Last().burst, temp.Last().priority, start, end));
            temp.RemoveAt(temp.Count - 1);

            while (temp.Count > 0)
            {
                if (temp.Last().arrival > (output.Last().start + min(quantum, output.Last().burst)))
                {
                    if (front == rear)
                    {
                        if (temp.Last().arrival < output.Last().end)
                        {
                            subtemp[rear] = new Process(output.Last());
                            subtemp[rear].burst -= temp.Last().arrival - output.Last().start;
                            rear = (rear + 1) % (nprocess + 1);
                            output.Last().burst = temp.Last().arrival - output.Last().start;
                            output.Last().end = temp.Last().arrival;
                        }
                        start = temp.Last().arrival;
                        end = temp.Last().arrival + temp.Last().burst;
                        output.Add(new Process(temp.Last().id, temp.Last().arrival, temp.Last().burst, temp.Last().priority, start, end));
                        temp.RemoveAt(temp.Count - 1);
                    }
                    else
                    {
                        if (output.Last().burst > quantum)
                        {
                            subtemp[rear] = new Process(output.Last());
                            subtemp[rear].burst -= quantum;
                            rear = (rear + 1) % (nprocess + 1);
                            output.Last().burst = quantum;
                            output.Last().end = output.Last().start + quantum;
                        }
                        start = output.Last().end;
                        end = start + subtemp[front].burst;
                        output.Add(new Process(subtemp[front].id, subtemp[front].arrival, subtemp[front].burst, subtemp[front].priority, start, end));
                        front = (front + 1) % (nprocess + 1);
                    }
                }
                else
                {
                    subtemp[rear] = (temp.Last());
                    rear = (rear + 1) % (nprocess + 1);
                    temp.RemoveAt(temp.Count - 1);
                }
            }

            while (rear != front)
            {
                if (output.Last().burst > quantum)
                {
                    subtemp[rear] = new Process(output.Last());
                    subtemp[rear].burst -= quantum;
                    output.Last().burst = quantum;
                    output.Last().end = output.Last().start + quantum;
                    rear = (rear + 1) % (nprocess + 1);
                }
                start = output.Last().end;
                end = start + subtemp[front].burst;
                output.Add(new Process(subtemp[front].id, subtemp[front].arrival, subtemp[front].burst, subtemp[front].priority, start, end));
                front = (front + 1) % (nprocess + 1);
            }
        }

        public void insert_idle()
        {
            int n = output.Count;
            if (output[0].arrival > 0) ++n;
            for (int i = 1; i < output.Count; ++i)
                if (output[i].arrival > output[i - 1].end) ++n;

            output_with_idle = new List<Process>(n);

            if (output[0].arrival > 0)
                output_with_idle.Add(new Process(input.Count + 1, 0, output[0].arrival, 0, 0, output[0].arrival));
                
            output_with_idle.Add(output[0]);

            for (int i = 1; i < output.Count; ++i)
            {
                if (output[i].arrival > output[i - 1].end)
                    output_with_idle.Add(new Process(input.Count + 1, output[i - 1].end, output[i].arrival - output[i - 1].end, 0, output[i - 1].end, output[i].arrival));

                output_with_idle.Add(output[i]);
            }

        }
    }

}



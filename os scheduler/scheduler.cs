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
                if (x.arrival == y.arrival) return 1;//swap
                else if (x.arrival < y.arrival) return 1;//swap
                else return -1;
            });
        }

        public void fcfs()
        {
            fcfs_sort(ref input);
            //input.Reverse();
            output.Add(new Process(input[input.Count - 1].id, input[input.Count - 1].arrival, input[input.Count - 1].burst,
                0, input[input.Count - 1].arrival, input[input.Count - 1].arrival + input[input.Count - 1].burst));

            for (int i = input.Count - 2; i > -1; --i)
            {
                if (input[i].arrival < output.Last().end)
                {
                    output.Add(new Process(input[i].id, input[i].arrival , input[i].burst,
                        0, output.Last().start + output.Last().burst,
                         output.Last().start + output.Last().burst + input[i].burst
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

        static int min(int x, int y) { return (x > y) ? y : x; }

        public void RR(int quantum)
        {
            List<Process> temp = new List<Process>(input);
            List<Process> subtemp = new List<Process>(nprocess);
            for (int i = 0; i <= nprocess; ++i)
            {
                subtemp.Add(new Process(0, 0, 0));
            }

            int front = 0, rear = 0;

            fcfs_sort(ref temp);

            start = temp.Last().arrival;
            end = 0;
            while (temp.Count > 0 && temp.Last().burst < quantum)
            {
                end = temp.Last().arrival + temp.Last().burst;
                output.Add(new Process(temp.Last().id, temp.Last().arrival, temp.Last().burst, temp.Last().priority, start, end));
                temp.RemoveAt(temp.Count - 1);
                start = end;
            }

            if (temp.Count > 0)
            {
                end += quantum;
                output.Add(new Process(temp.Last().id, temp.Last().arrival, temp.Last().burst, temp.Last().priority, start, end));
                if (temp.Last().burst > quantum)
                {
                    output.Last().burst = quantum;
                    temp.Last().burst -= quantum;
                    temp.Last().arrival = end;
                    fcfs_sort(ref temp);
                }
                else
                    temp.RemoveAt(temp.Count - 1);
            }

            while (temp.Count > 0)
            {
                if (temp.Last().arrival > output.Last().end)
                {
                    if (front != rear)
                    {
                        start = end;
                        end += min(subtemp[front].burst, quantum);
                        output.Add(new Process(subtemp[front].id, subtemp[front].arrival, subtemp[front].burst, subtemp[front].priority, start, end));
                        if (subtemp[front].burst > quantum)
                        {
                            output.Last().burst = quantum;
                            subtemp[front].burst -= quantum;
                            subtemp[front].arrival = end;
                            temp.Add(subtemp[front]);
                            fcfs_sort(ref temp);
                        }
                        front = (front + 1) % (nprocess + 1);
                    }
                    else
                    {
                        start = temp.Last().arrival;
                        end = temp.Last().arrival + min(temp.Last().burst, quantum);
                        output.Add(new Process(temp.Last().id, temp.Last().arrival, temp.Last().burst, temp.Last().priority, start, end));
                        if (temp[front].burst > quantum)
                        {
                            output.Last().burst = quantum;
                            temp.Last().burst -= quantum;
                            temp.Last().arrival = end;
                            fcfs_sort(ref temp);
                        }
                        else
                            temp.RemoveAt(temp.Count - 1);
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
                start = end;
                end += min(subtemp[front].burst, quantum);
                output.Add(new Process(subtemp[front].id, subtemp[front].arrival, subtemp[front].burst, subtemp[front].priority, start, end));
                if (subtemp[front].burst > quantum)
                {
                    output.Last().burst = quantum;
                    subtemp[front].burst -= quantum;
                    subtemp[rear] = (subtemp[front]);
                    rear = (rear + 1) % (nprocess + 1);
                }
                front = (front + 1) % (nprocess + 1);
            }
        }
    }

}



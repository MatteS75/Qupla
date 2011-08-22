using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;

namespace Qupla.IndicatorServer.TrayClient
{
    public class IndicatorStates : ObservableCollection<IndicatorState>
    {
        private readonly IClient _client;

        public IndicatorStates() : this(new Client(new AppConfigSettings())) {}

        public IndicatorStates(IClient client)
        {
            _client = client;
            LoadData();
            StartTimer();
        }

        private void StartTimer()
        {
            var timer = new DispatcherTimer();
            timer.Tick += (s, e) => LoadData();
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Start();
        }

        private void LoadData()
        {
            IEnumerable<IndicatorState> indicatorStates;
            try
            {
                indicatorStates = _client.GetIndicatorStates();
            }
            catch (Exception e)
            {
                indicatorStates = new[]
                                      {
                                          new IndicatorState
                                              {
                                                  Color = "Red",
                                                  Content = e.ToString(),
                                                  Message = e.Message,
                                                  Name = "Client exception",
                                                  UpdateTime = DateTime.Now
                                              }
                                      };
            }
            var list = indicatorStates.ToList();
            list.ForEach(i => i.RequestTime = DateTime.Now);
            Clear();
            list.ForEach(Add);
        }
    }
}
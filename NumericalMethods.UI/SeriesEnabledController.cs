using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace NumericalMethods.UI
{
    internal class SeriesEnabledController
    {
        private readonly IReadOnlyList<Series> _series;

        private readonly OptionSwitcher _optionSwitcher;

        public SeriesEnabledController(IReadOnlyList<Series> series, bool needLoopOptions = true)
        {
            _series = series ?? throw new ArgumentNullException(nameof(series));

            _optionSwitcher = _series.Count >= 2
                ? new OptionSwitcher(_series.Count + 1)
                {
                    NeedLoopOptions = needLoopOptions
                }: null;
        }

        private void ExecuteSwitch(int operationId)
        {
            bool allEnabled = operationId < 1;
            foreach (Series element in _series)
                element.Enabled = allEnabled;
            
            if (!allEnabled)
                _series[operationId - 1].Enabled = true;
        }

        public void SwitchNext()
        {
            if (_optionSwitcher?.MoveNext() ?? false)
                ExecuteSwitch(_optionSwitcher.CurrentOption);
        }

        public void SwitchPrevious()
        {
            if (_optionSwitcher?.MovePrevious() ?? false)
                ExecuteSwitch(_optionSwitcher.CurrentOption);
        }
    }
}
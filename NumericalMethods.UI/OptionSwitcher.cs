using System;

namespace NumericalMethods.UI
{
    internal class OptionSwitcher
    {
        public int OptionsCount { get; }

        public int CurrentOption { get; private set; }

        public bool NeedLoopOptions { get; set; } = false;

        public OptionSwitcher(int optionsCount, int defaultOption = 0)
        {
            if (optionsCount < 1)
                throw new ArgumentOutOfRangeException(nameof(optionsCount), "OptionsCount must be greater than zero.");

            if (defaultOption < 0 || defaultOption >= optionsCount)
                throw new ArgumentOutOfRangeException(nameof(defaultOption), $"DefaultOption must be in range [0; {nameof(OptionsCount)}].");

            OptionsCount = optionsCount;
            CurrentOption = defaultOption;
        }

        public bool MoveNext()
        {
            var next = CurrentOption + 1;

            if (NeedLoopOptions && next >= OptionsCount)
                next = 0;

            if (next >= OptionsCount)
                return false;

            CurrentOption = next;
            return true;
        }

        public bool MovePrevious()
        {
            var prev = CurrentOption - 1;

            if (NeedLoopOptions && prev < 0)
                prev = OptionsCount - 1;

            if (prev < 0)
                return false;

            CurrentOption = prev;
            return true;
        }
    }
}
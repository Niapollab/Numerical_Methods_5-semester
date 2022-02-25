using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumericalMethods.UI
{
    public static class ChartVisualiser
    {
        public static Task<DialogResult> RunAsync<T>(Func<T> chartVisualiserFormBuilder = default, CancellationToken cancellationToken = default)
            where T : ChartVisualiserForm
        {
            _ = chartVisualiserFormBuilder ?? throw new ArgumentNullException(nameof(chartVisualiserFormBuilder));

            return FormRunner.RunAsync(chartVisualiserFormBuilder, cancellationToken);
        }

        public static Task<DialogResult> RunAsync(IEnumerable<ChartFunctionInfo> chartFunctionsInfo, CancellationToken cancellationToken = default)
        {
            _ = chartFunctionsInfo ?? throw new ArgumentNullException(nameof(chartFunctionsInfo));

            return RunAsync(() => new ChartVisualiserForm(chartFunctionsInfo), cancellationToken);
        }

        public static Task<DialogResult> RunAsync(CancellationToken cancellationToken = default, params ChartFunctionInfo[] chartFunctionsInfo)
        {
            _ = chartFunctionsInfo ?? throw new ArgumentNullException(nameof(chartFunctionsInfo));

            return RunAsync(chartFunctionsInfo, cancellationToken);
        }

        public static Task<DialogResult> RunAsync(params ChartFunctionInfo[] chartFunctionsInfo)
        {
            _ = chartFunctionsInfo ?? throw new ArgumentNullException(nameof(chartFunctionsInfo));

            return RunAsync((IEnumerable<ChartFunctionInfo>)chartFunctionsInfo);
        }

        public static DialogResult Run<T>(Func<T> chartVisualiserFormBuilder = default)
            where T : ChartVisualiserForm
        {
            _ = chartVisualiserFormBuilder ?? throw new ArgumentNullException(nameof(chartVisualiserFormBuilder));

            return FormRunner.Run(chartVisualiserFormBuilder);
        }

        public static DialogResult Run(IEnumerable<ChartFunctionInfo> chartFunctionsInfo)
        {
            _ = chartFunctionsInfo ?? throw new ArgumentNullException(nameof(chartFunctionsInfo));

            return Run(() => new ChartVisualiserForm(chartFunctionsInfo));
        }

        public static DialogResult Run(params ChartFunctionInfo[] chartFunctionsInfo)
        {
            _ = chartFunctionsInfo ?? throw new ArgumentNullException(nameof(chartFunctionsInfo));

            return Run((IEnumerable<ChartFunctionInfo>)chartFunctionsInfo);
        }
    }
}
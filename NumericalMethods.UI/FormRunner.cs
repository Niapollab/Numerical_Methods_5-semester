using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumericalMethods.UI
{
    internal static class FormRunner
    {
        static FormRunner()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        public static DialogResult Run<T>(Func<T> formBuilder)
            where T : Form
        {
            _ = formBuilder ?? throw new ArgumentNullException(nameof(formBuilder));

            return RunAsync(formBuilder).Result;
        }

        public static Task<DialogResult> RunAsync<T>(Func<T> formBuilder, CancellationToken cancellationToken = default)
        where T : Form
        {
            _ = formBuilder ?? throw new ArgumentNullException(nameof(formBuilder));

            var tcs = new TaskCompletionSource<DialogResult>();
            var thread = new Thread(() =>
            {
                try
                {
                    using T form = formBuilder();

                    using (cancellationToken.Register(() =>
                    {
                        form.Close();
                        tcs.TrySetCanceled();
                    }, useSynchronizationContext: true))
                    {
                        Application.Run(form);
                        tcs.TrySetResult(form.DialogResult);
                    }
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            })
            {
                IsBackground = true
            };

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            return tcs.Task;
        }
    }
}
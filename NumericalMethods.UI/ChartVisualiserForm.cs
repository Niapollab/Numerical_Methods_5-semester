using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace NumericalMethods.UI
{
    public partial class ChartVisualiserForm : Form
    {
        private readonly SeriesEnabledController _seriesEnabledController;
        private readonly IEnumerable<ChartFunctionInfo> _functionsInfo;
        private readonly Chart _chart;
        private readonly ToolTip _coordsTooltip;
        private readonly bool _needDrawLegend;

        public ChartVisualiserForm(IEnumerable<ChartFunctionInfo> functionsInfo)
        {
            _functionsInfo = functionsInfo ?? throw new ArgumentNullException(nameof(functionsInfo));

            InitializeComponent();

            _coordsTooltip = new ToolTip();

            _needDrawLegend = functionsInfo.Any(info => !string.IsNullOrEmpty(info.Name));

            _chart = BuildChart();
            _seriesEnabledController = new SeriesEnabledController(_chart.Series, _chart.ChartAreas[0].RecalculateAxesScale);
            Controls.Add(_chart);

            _chart.KeyDown += CtrlSEntered;
            _chart.KeyDown += HClicked;
            _chart.MouseMove += OnPoint;
            _chart.PreviewKeyDown += EnableLeftRightArrow;
            _chart.KeyDown += LeftRightArrowClicked;
        }

        public ChartVisualiserForm(params ChartFunctionInfo[] functionsInfo)
            : this((IEnumerable<ChartFunctionInfo>)functionsInfo)
        {
        }

        private Chart BuildChart()
        {
            var chart = new Chart
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Size = Size
            };

            chart.Legends.Add(new Legend()
            {
                Enabled = _needDrawLegend
            });
            chart.ChartAreas.Add(new ChartArea());

            foreach (ChartFunctionInfo functionInfo in _functionsInfo)
            {
                var series = new Series
                {
                    BorderWidth = functionInfo.BorderWidth,
                    Color = functionInfo.Color,
                    IsXValueIndexed = true,
                    IsVisibleInLegend = false,
                    BorderDashStyle = functionInfo.ChartDashStyle,
                    ChartType = SeriesChartType.Spline
                };

                if (!string.IsNullOrEmpty(functionInfo.Name))
                {
                    series.Name = functionInfo.Name;
                    series.IsVisibleInLegend = true;
                }

                chart.Series.Add(series);

                foreach (var point in functionInfo.Points)
                    series.Points.AddXY(point.X, point.Y);
            }

            return chart;
        }

        private void CtrlSEntered(object sender, KeyEventArgs eventArgs)
        {
            if (eventArgs.KeyData != (Keys.Control | Keys.S))
                return;
            
            var imageSize = new Size(1920, 1080);

            using var saveDialog = new SaveFileDialog()
            {
            };

            ExecuteWhileFormHidden(() =>
            {
                var oldSize = _chart.Size;

                _chart.Size = imageSize;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                    _chart.SaveImage(saveDialog.FileName, GetChartImageFormatByExtension(saveDialog.FileName));

                _chart.Size = oldSize;
            });
        }

        private void OnPoint(object sender, MouseEventArgs eventArgs)
        {
            Point location = eventArgs.Location;

            _coordsTooltip.RemoveAll();

            HitTestResult hitTestResult = _chart.HitTest(location.X, location.Y, ChartElementType.DataPoint);

            if (hitTestResult.Object is not DataPoint datePoint)
                return;

            _coordsTooltip.Show($"X={datePoint.XValue}; Y={datePoint.YValues[0]}", _chart, location.X, location.Y - 15);
        }

        private void HClicked(object sender, KeyEventArgs eventArgs)
        {
            if (!_needDrawLegend || eventArgs.KeyData != Keys.H)
                return;

            _chart.Legends[0].Enabled = !_chart.Legends[0].Enabled;
        }

        private void LeftRightArrowClicked(object sender, KeyEventArgs eventArgs)
        {
            switch (eventArgs.KeyData)
            {
                case Keys.Left:
                    _seriesEnabledController.SwitchNext();
                    break;
                case Keys.Right:
                    _seriesEnabledController.SwitchPrevious();
                    break;
            }
        }

        private void EnableLeftRightArrow(object sender, PreviewKeyDownEventArgs eventArgs)
        {
            switch (eventArgs.KeyData)
            {
                case Keys.Left:
                case Keys.Right:
                    eventArgs.IsInputKey = true;
                    break;
            }
        }

        private static ChartImageFormat GetChartImageFormatByExtension(string filename)
            => Path.GetExtension(filename.ToLower()) switch
            {
                ".png" => ChartImageFormat.Png,
                ".bmp" => ChartImageFormat.Bmp,
                _ => ChartImageFormat.Jpeg
            };

        private void ExecuteWhileFormHidden(Action action)
        {
            Visible = false;
            SuspendLayout();

            action();

            ResumeLayout(false);
            Visible = true;
        }
    }
}
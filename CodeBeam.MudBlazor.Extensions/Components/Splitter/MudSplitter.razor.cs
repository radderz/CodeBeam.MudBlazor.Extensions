﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Extensions;
using MudBlazor.Utilities;

namespace MudExtensions
{
    /// <summary>
    /// Split two panels with a bar.
    /// </summary>
    public partial class MudSplitter : MudComponentBase
    {

        readonly Guid _styleGuid = Guid.NewGuid();
        MudSlider<double> _slider = new();

        /// <summary>
        /// 
        /// </summary>
        protected string? Classname => new CssBuilder("mud-splitter")
            .AddClass($"border-solid border-2 mud-border-{Color.ToDescriptionString()}", Bordered)
            .AddClass($"mud-splitter-generate mud-splitter-generate-{_styleGuid}")
            .AddClass(Class)
            .Build();

        /// <summary>
        /// 
        /// </summary>
        protected string? ContentClassname => new CssBuilder($"mud-splitter-content mud-splitter-content-{_styleGuid} d-flex")
            .AddClass("ma-2", EnableMargin)
            .AddClass(ClassContent)
            .Build();

        /// <summary>
        /// 
        /// </summary>
        protected string? SliderClassname => new CssBuilder($"mud-splitter-thumb mud-splitter-thumb-{_styleGuid} mud-splitter-track")
            .AddClass("mud-splitter-thumb-disabled", EnableSlide == false)
            .Build();

        /// <summary>
        /// The two contents' (sections) classes, seperated by space.
        /// </summary>
        [Parameter]
        public string? ClassContent { get; set; }

        //string _height;
        /// <summary>
        /// The height of splitter. For example: "400px"
        /// </summary>
        /// <remarks>The default is 100%</remarks>
        [Parameter]
        public string? Height { get; set; }

        /// <summary>
        /// The height of splitter.
        /// </summary>
        [Parameter]
        public Color Color { get; set; }

        /// <summary>
        /// If true, splitter has borders.
        /// </summary>
        [Parameter]
        public bool Bordered { get; set; }

        /// <summary>
        /// The style to apply to both content sections, seperated by space.
        /// </summary>
        [Parameter]
        public string? ContentStyle { get; set; }

        /// <summary>
        /// The style of the <see cref="StartContent"/>, seperated by space.
        /// </summary>
        [Parameter]
        public string? StartContentStyle { get; set; }

        /// <summary>
        /// The style of the <see cref="EndContent"/>, seperated by space.
        /// </summary>
        [Parameter]
        public string? EndContentStyle { get; set; }

        /// <summary>
        /// The splitter bar's styles, seperated by space. The style string should end with: "!important;"
        /// </summary>
        /// <remarks>The default is 2px</remarks>
        [Parameter]
        public string? BarStyle { get; set; } = "width:2px !important;";

        /// <summary>
        /// The slide sensitivity that should between 0.01 and 10. Smaller values increase the smooth but reduce performance. Default is 0.1
        /// </summary>
        [Parameter]
        public double Sensitivity { get; set; } = 0.1d;

        /// <summary>
        /// If true, user can interact with splitter bar.
        /// </summary>
        /// <remarks>The default is true</remarks>
        [Parameter]
        public bool EnableSlide { get; set; } = true;

        /// <summary>
        /// Enables the default margin.
        /// </summary>
        /// <remarks>The default is true, which adds class: "ma-2"</remarks>
        [Parameter]
        public bool EnableMargin { get; set; } = true;

        ///// <summary>
        ///// If true, splitter bar goes vertical.
        ///// </summary>
        //[Parameter]
        //public bool Horizontal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public RenderFragment? StartContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public RenderFragment? EndContent { get; set; }

        /// <summary>
        /// The start content's percentage.
        /// Default is 50.
        /// </summary>
        /// <remarks>The default is 50</remarks>
        [Parameter]
        public double Dimension { get; set; } = 50;

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public EventCallback<double> DimensionChanged { get; set; }

        /// <summary>
        /// Fires when double click.
        /// </summary>
        [Parameter]
        public EventCallback OnDoubleClicked { get; set; }


        string? EffectiveStartStyle { get { return !string.IsNullOrWhiteSpace(StartContentStyle) ? StartContentStyle : ContentStyle; }}
        string? EffectiveEndStyle { get { return !string.IsNullOrWhiteSpace(EndContentStyle) ? EndContentStyle : ContentStyle; } }
        string? EffectiveHeight { get { return !string.IsNullOrWhiteSpace(Height) ? $"height:{Height} !important;" : null; } }
        string? EffectiveBarStyle { get => BarStyle; }
        string? EffectiveColor { get { return $"background-color:var(--mud-palette-{(Color == Color.Default ? "action-default" : Color.ToDescriptionString())}) !important;"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="percentage"></param>
        /// <returns></returns>
        protected async Task UpdateDimension(double percentage)
        {
            Dimension = percentage;

            if (Dimension < 0)
                Dimension = 0;
            else if (Dimension > 100)
                Dimension = 100;

            if (DimensionChanged.HasDelegate)
            {
                await DimensionChanged.InvokeAsync(percentage);
            }

            //return Task.CompletedTask;
        }
        
        private async Task OnDoubleClick()
        {
            if (OnDoubleClicked.HasDelegate)
                await OnDoubleClicked.InvokeAsync();

            //return Task.CompletedTask;
        }
    }
}

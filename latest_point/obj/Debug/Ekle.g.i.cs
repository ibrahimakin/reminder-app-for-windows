﻿#pragma checksum "..\..\Ekle.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1A2FC80B6F5FDA958F00DF87B1CFC32BF939ACD80C611393DE982D58554D3192"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using latest_point;


namespace latest_point {
    
    
    /// <summary>
    /// Ekle
    /// </summary>
    public partial class Ekle : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\Ekle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame yeniEkle;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\Ekle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton checkKitap;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\Ekle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton checkVideo;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Ekle.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton checkBasvuru;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/latest_point;component/ekle.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Ekle.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.yeniEkle = ((System.Windows.Controls.Frame)(target));
            return;
            case 2:
            this.checkKitap = ((System.Windows.Controls.RadioButton)(target));
            
            #line 16 "..\..\Ekle.xaml"
            this.checkKitap.Checked += new System.Windows.RoutedEventHandler(this.CheckKitap_Checked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.checkVideo = ((System.Windows.Controls.RadioButton)(target));
            
            #line 17 "..\..\Ekle.xaml"
            this.checkVideo.Checked += new System.Windows.RoutedEventHandler(this.CheckVideo_Checked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.checkBasvuru = ((System.Windows.Controls.RadioButton)(target));
            
            #line 18 "..\..\Ekle.xaml"
            this.checkBasvuru.Checked += new System.Windows.RoutedEventHandler(this.CheckBasvuru_Checked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


// =====================================================================================================
// 13_WPF_BASICS.cs
// -----------------------------------------------------------------------------------------------------
// PURPOSE:
// Introduce the fundamentals of Windows Presentation Foundation (WPF) - the primary framework
// for creating Windows desktop applications with modern UI capabilities.
//
// WPF separates *presentation* (XAML) from *logic* (C#), enabling clean architecture, styling,
// and event-driven programming.
// -----------------------------------------------------------------------------------------------------
//
// LESSON OBJECTIVES:
// - Understand what WPF is and how it differs from WinForms
// - Learn the structure of a WPF project (App.xaml, MainWindow.xaml, MainWindow.xaml.cs)
// - Understand XAML basics (controls, layout panels, properties, and events)
// - Build a simple interactive window using C# code-behind
// -----------------------------------------------------------------------------------------------------
//
// NOTE:
// This code can be compiled as a standalone example, but in a real WPF project,
// you should use Visual Studio's "WPF App" template (which auto-generates App.xaml).
// -----------------------------------------------------------------------------------------------------

// Required Namespaces
using System;                    // Base .NET functionality
using System.Windows;             // Core WPF application types (Application, Window)
using System.Windows.Controls;    // UI controls (Button, TextBox, etc.)
using System.Windows.Input;       // Keyboard/mouse input events (optional for advanced use)
using System.Windows.Media;       // Colours, brushes, and drawing elements
using System.Windows.Shapes;      // For shapes like Rectangle, Ellipse, Line (optional)

namespace CS_Zero_Hero.wpf
{
    // -------------------------------------------------------------------------------------------------
    // ENTRY POINT - WPF Application
    // -------------------------------------------------------------------------------------------------
    // WPF requires [STAThread] because it depends on COM single-threaded apartment model.
    // The App class initializes and runs the main window.
    // -------------------------------------------------------------------------------------------------
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            // Create and run the WPF application manually
            App app = new App();
            app.Run(new MainWindow());
        }
    }

    // -------------------------------------------------------------------------------------------------
    // MAIN WINDOW
    // -------------------------------------------------------------------------------------------------
    // In a full WPF project:
    //   - MainWindow.xaml defines the UI layout
    //   - MainWindow.xaml.cs contains the code-behind logic
    //
    // For this example, we build everything in code to make it self-contained.
    // -------------------------------------------------------------------------------------------------

    public partial class MainWindow : Window
    {
        public TextBox inputBox;
        private TextBlock outputText;
        private Button greetingButton;

        public MainWindow()
        {
            this.inputBox = new TextBox();
            this.Title = "This is a C# WPF Demo!";
            this.Width = 500;
            this.Height = 500;
            this.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            // Layout and alignment (center)
            StackPanel layout = new StackPanel
            {
                Margin = new Thickness(20),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Label 
            TextBlock label = new TextBlock
            {
                Text = "Enter your name:",
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(5)
            };

            inputBox = new TextBox
            {
                Width = 250,
                Height = 50,
                Margin = new Thickness(0, 0, 0, 10),
            };
            greetingButton = new Button
            {
                Content = "Greet Me!",
                Width = 100,
                Height = 25,
                Background = Brushes.LightGray,
                Margin = new Thickness(0, 0, 0, 50),
            };

            greetingButton.Click += OnGreetButtonClick;

            outputText = new TextBlock
            {
                FontSize = 10,
                Foreground = Brushes.Blue,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center

            };

            layout.Children.Add(label);
            layout.Children.Add(inputBox);
            layout.Children.Add(outputText);
            layout.Children.Add(greetingButton);

            this.Content = layout;

        }

        private void OnGreetButtonClick(object sender, RoutedEventArgs e)
        {
            string name = inputBox.Text.Trim();
            //string[] process = name.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            //string.Join(" ", process);


            if (string.IsNullOrWhiteSpace(name))
            {
                outputText.Text = "Please enter your name!";
                outputText.Foreground = Brushes.DarkRed;

            }
            else
            {
                outputText.Text = $"Hello {name}";
                outputText.Foreground = Brushes.DarkRed;

            }

        }
    }
    }

namespace Voyage.Mobie

open System
open Xamarin.Forms

module Wallets =

    let entryRow txtEntry numericEntry = 
        let grid = new Grid()
        grid.RowDefinitions.Add(new RowDefinition())
        grid.ColumnDefinitions.Add(new ColumnDefinition(Width = GridLength(3., GridUnitType.Star)))
        grid.ColumnDefinitions.Add(new ColumnDefinition(Width = GridLength(1., GridUnitType.Star)))
        grid.Children.Add(txtEntry, 0, 0)
        grid.Children.Add(numericEntry, 1, 0)
        grid

    let button handler = 
        let btn = new Button(Text = "Add")
        btn.Clicked.AddHandler handler
        btn

    let entry placeholder keyboardType = 
        new Entry(
            Placeholder = placeholder,
            Keyboard = keyboardType)

    let entryNumeric() = 
        entry "Price" Keyboard.Numeric

    let entryText() = 
        entry "Text" Keyboard.Text

    let stackLayout() =
        let mainGrid = new Grid()
        let layout = new StackLayout()
        let entry = entryText()
        let price = entryNumeric()
        let handler =
            new EventHandler (fun _ _ -> 
                let rowIndex = mainGrid.Children.Count / 2
                mainGrid.Children.Add(new Label(Text = entry.Text), 1, rowIndex)
                mainGrid.Children.Add(new Label(Text = price.Text), 2, rowIndex))

        layout.Children.Add(entryRow entry price)
        layout.Children.Add(button handler)
        layout.Children.Add(new ScrollView (Content = mainGrid))
        layout

    let page title = 
        new ContentPage(Title = title, Content = stackLayout())

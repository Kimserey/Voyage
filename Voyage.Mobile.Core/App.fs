namespace Voyage.Mobie

open System
open Xamarin.Forms

[<AutoOpen>]
module Domain =
    type Wallet = {
        Owner: Owner
        Expenses: Expense list
    } 
    and Owner = Owner of string
    and Expense = {
        Title: string
        Amount: decimal
        Date: DateTime
    }

module Repository =
    // Test data
    let getWallets() =
        [ { Owner = Owner "Sry"
            Expenses = 
                [
                    { Title = "Bagel"
                      Amount = 2.m
                      Date = DateTime.Parse("2016-10-10") }
                    { Title = "Toast"
                      Amount = 2.m
                      Date = DateTime.Parse("2016-10-11") }
                    { Title = "Shows"
                      Amount = 34.m
                      Date = DateTime.Parse("2016-10-12") }
                ] }
          { Owner = Owner "Zx" 
            Expenses = 
                [
                    { Title = "Cinema"
                      Amount = 14.m
                      Date = DateTime.Parse("2016-10-10") }
                    { Title = "Food"
                      Amount = 30.m
                      Date = DateTime.Parse("2016-10-11") }
                    { Title = "Shows"
                      Amount = 34.m
                      Date = DateTime.Parse("2016-10-12") }
                ] } ]

module Wallets =

    type WalletCell =
        | FullDate of string
        | OwnerHeader of Owner list
        | Total of decimal list
        | ExpenseLine of ExpenseTitle * (ExpenseAmount list)
    and ExpenseTitle = ExpenseTitle of string
    and ExpenseAmount = ExpenseAmount of decimal

    let getExpenseRows() =
        let wallets = Repository.getWallets()
        let owners = wallets |> List.map (fun w -> w.Owner)
        wallets
        |> List.collect (fun w -> w.Expenses |> List.map (fun exp -> w.Owner, exp))
        |> List.sortBy (fun (_, exp) -> exp.Date)
        |> List.map (fun (owner, expense) -> ExpenseLine(ExpenseTitle expense.Title, [0..owners.Length - 1] |> List.map (fun i -> if owners.[i] = owner then ExpenseAmount expense.Amount else ExpenseAmount 0.m)))
    
    let makeExpenseGridRow row =
        let grid = new Grid()
        grid.RowDefinitions.Add(new RowDefinition())
        
        match row with
        | FullDate date -> 
            grid.Children.Add(new Label(Text = date), 0, 0)
        
        | OwnerHeader owners ->
            grid.ColumnDefinitions.Add(new ColumnDefinition(Width = GridLength(3., GridUnitType.Star)))
            owners
            |> List.iteri (fun idx (Owner name) -> 
                grid.ColumnDefinitions.Add(new ColumnDefinition(Width = GridLength(1., GridUnitType.Star)))
                grid.Children.Add(new Label(Text = name), idx + 1, 0))
            
        | ExpenseLine (ExpenseTitle title, amounts) ->
            grid.ColumnDefinitions.Add(new ColumnDefinition(Width = GridLength(3., GridUnitType.Star)))
            grid.Children.Add(new Label(Text = title), 0, 0)
            amounts
            |> List.iteri (fun idx (ExpenseAmount amount) ->
                grid.ColumnDefinitions.Add(new ColumnDefinition(Width = GridLength(1., GridUnitType.Star)))
                grid.Children.Add(new Label(Text = string amount), idx + 1, 0))
            
        | Total totals ->
            grid.ColumnDefinitions.Add(new ColumnDefinition(Width = GridLength(3., GridUnitType.Star)))
            totals
            |> List.iteri (fun idx amount -> 
                grid.ColumnDefinitions.Add(new ColumnDefinition(Width = GridLength(1., GridUnitType.Star)))
                grid.Children.Add(new Label(Text = string amount), idx + 1, 0))
        
        grid

    let makeStackLayout() =
        let layout = new StackLayout()
        (OwnerHeader [ Owner "Sry"; Owner "Zx" ])::getExpenseRows()
        |> List.iter (makeExpenseGridRow >> layout.Children.Add)
        new ScrollView(Content = layout)

    let page = 
        new ContentPage(Title = "Wallets", Content = makeStackLayout())

module Main =
    type App() =
        inherit Application(MainPage = Wallets.page)
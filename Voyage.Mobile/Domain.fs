namespace Voyage.Mobie

open System

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
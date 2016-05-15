namespace Voyage.Mobie

open Xamarin.Forms

module Main =

    type App() =
        inherit Application(MainPage = Voyage.Mobie.Wallets.page())
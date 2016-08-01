﻿using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TorrentBrowser
{
    public class FakeTorrentBrowserWorker
    {
        private static Random rnd = new Random();

        public IObservable<TorrentMovie> Work()
        {
            var min = 500;
            var max = 1000;
            return Observable.Create<TorrentMovie>(observer =>
            {
                observer.OnNext(NewMethod());
                Thread.Sleep(rnd.Next(min, max));
                observer.OnNext(NewMethod());
                Thread.Sleep(rnd.Next(min, max));
                observer.OnNext(NewMethod());
                Thread.Sleep(rnd.Next(min, max));
                observer.OnNext(NewMethod());
                Thread.Sleep(rnd.Next(min, max));
                observer.OnNext(NewMethod());
                Thread.Sleep(rnd.Next(min, max));
                observer.OnNext(NewMethod());
                Thread.Sleep(rnd.Next(min, max));
                observer.OnNext(NewMethod());
                Thread.Sleep(rnd.Next(min, max));
                observer.OnNext(NewMethod());
                Thread.Sleep(rnd.Next(min, max));
                observer.OnNext(NewMethod());
                Thread.Sleep(rnd.Next(min, max));
                observer.OnNext(NewMethod());
                Thread.Sleep(rnd.Next(min, max));
                observer.OnNext(NewMethod());
                observer.OnCompleted();
                Thread.Sleep(1000);
                return Disposable.Create(() => Console.WriteLine("Observer has unsubscribed"));
            });
        }

        private static TorrentMovie NewMethod()
        {
            return new TorrentMovie()
            {
                Movie = "Test1",
                Rating = 9.0F,
                ImdbLink = new Uri("http://www.imdb.com/title/tt3385516/"),
                PictureUrl = new Uri("http://ia.media-imdb.com/images/M/MV5BMTc3NTUzNTI4MV5BMl5BanBnXkFtZTgwNjU0NjU5NzE@._V1_UX182_CR0,0,182,268_AL_.jpg")
            };
        }
    }
}
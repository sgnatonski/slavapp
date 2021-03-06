﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace TorrentBrowser
{
    public static class ImdbDataExtractor
    {
        public static async Task<ImdbData> ExtractData(int imdbId, CancellationToken cancellationToken)
        {
            var imdbPage = await PirateRequest.OpenAsync(new Uri($"http://www.imdb.com/title/tt{imdbId}/"), cancellationToken);
            var imdbQueries = new ImdbQueryProvider();

            var originalTitle = imdbPage.QuerySelector(imdbQueries.OriginalTitleQuery)?.TextContent.Replace("(original title)", "");
            var movieName = originalTitle != null 
                ? $"{originalTitle} ({imdbPage.QuerySelector(imdbQueries.YearQuery)?.TextContent})"
                : imdbPage.QuerySelector(imdbQueries.TitleQuery)?.TextContent;
            var rating = imdbPage.QuerySelector(imdbQueries.RatingQuery)?.TextContent;
            var pictureUrl = imdbPage.QuerySelector(imdbQueries.PictureQuery)?.GetAttribute("src");

            return new ImdbData
            {
                Id = imdbId,
                MovieName = movieName,
                Rating = !string.IsNullOrEmpty(rating) ? (float?)float.Parse(rating, System.Globalization.CultureInfo.InvariantCulture) : null,
                PictureLink = pictureUrl != null ? new Uri(pictureUrl) : null
            };
        }
    }
}

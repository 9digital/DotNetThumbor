﻿namespace DotNetThumborTest.TestThumborFilters
{
    using System.Globalization;

    using DotNetThumbor;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class TestContrast
    {
        [Test]
        [TestCase(-100)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(99)]
        [TestCase(100)]
        [TestCase(1000)]
        public void ThumborContrastFilter(int contrast)
        {
            var thumbor = new Thumbor("http://localhost/");
            var resizedUrl = thumbor.BuildImage("http://localhost/image.jpg")
                                    .Contrast(contrast)
                                    .ToFullUrl();
            resizedUrl.Should().Be(string.Format("http://localhost/unsafe/filters:contrast({0})/http://localhost/image.jpg", contrast.ToString(CultureInfo.InvariantCulture)));
        }

        [Test]
        public void ThumborBrightnessFilterFirstIgnored()
        {
            var thumbor = new Thumbor("http://localhost/");
            var resizedUrl = thumbor.BuildImage("http://localhost/image.jpg")
                                    .Contrast(10)
                                    .Contrast(99)
                                    .ToFullUrl();
            resizedUrl.Should().Be("http://localhost/unsafe/filters:contrast(99)/http://localhost/image.jpg");
        }
    }
}

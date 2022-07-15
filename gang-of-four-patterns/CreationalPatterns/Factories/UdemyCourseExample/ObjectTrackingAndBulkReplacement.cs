using System;
using System.Collections.Generic;
using System.Text;

namespace Factories
{
    public interface ITheme
    {
        string TextColor { get; }
        string BrgColor { get; }
    }

    public class LightTheme : ITheme
    {
        public string TextColor => "black";
        public string BrgColor => "white";
    }

    public class DarkTheme : ITheme
    {
        public string TextColor => "white";
        public string BrgColor => "dark gray";
    }

    public class TrackingThemeFactory
    {
        private readonly List<WeakReference<ITheme>> _themes = new();

        public ITheme CreateTheme(bool dark)
        {
            ITheme theme = dark
                ? new DarkTheme()
                : new LightTheme();
            _themes.Add(new WeakReference<ITheme>(theme));
            return theme;
        }

        public string Info
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var reference in _themes)
                {
                    if (reference.TryGetTarget(out var theme))
                    {
                        bool dark = theme is DarkTheme;
                        sb.Append(dark ? "Dark" : "Light")
                            .AppendLine(" theme");
                    }
                }
                return sb.ToString();
            }
        }
    }

    public class Ref<T> where T : class
    {
        public T Value;

        public Ref(T value)
        {
            Value = value;
        }
    }

    public class ReplaceableThemeFactory
    {
        private readonly List<WeakReference<Ref<ITheme>>> _themes = new();

        private ITheme CreateThemeImpl(bool dark)
        {
            return dark
                ? new DarkTheme()
                : new LightTheme();
        }

        public Ref<ITheme> CreateTheme(bool dark)
        {
            var r = new Ref<ITheme>(CreateThemeImpl(dark));
            _themes.Add(new(r));
            return r;
        }

        public void ReplaceTheme(bool dark)
        {
            foreach (var wr in _themes)
            {
                if (wr.TryGetTarget(out var reference))
                {
                    reference.Value = CreateThemeImpl(dark);
                }
            }
        }
    }
}


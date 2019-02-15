using System;

namespace Thaoden.RestaurantReservation
{
    public class Maybe<T>
    {
        private readonly bool _hasValue;
        private readonly T _value;

        public Maybe()
        {
            _hasValue = false;
        }

        public Maybe(T value)
        {
            if (!value.GetType().IsValueType && value == default)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _hasValue = true;
            _value = value;
        }

        public Maybe<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return Match(
                none: new Maybe<TResult>(),
                some: x => new Maybe<TResult>(selector(x))
                );
        }

        public TResult Match<TResult>(TResult none, Func<T, TResult> some)
        {
            if(!none.GetType().IsValueType && none == default)
            {
                throw new ArgumentNullException(nameof(none));
            }
            if(some is null)
            {
                throw new ArgumentNullException(nameof(some));
            }

            return _hasValue ? some(_value) : none;
        }
    }
}

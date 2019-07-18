using Alessa.QueryBuilder.Entities;
using System;

namespace Alessa.QueryBuilder
{
    /// <summary>
    /// Struct to make comparisons in the specified values.
    /// </summary>
    internal struct ValueComparer
    {
        /// <summary>
        /// Teh original value.
        /// </summary>
        internal object Value;
        /// <summary>
        /// The field type.
        /// </summary>
        internal static EFieldType FieldType;
        /// <summary>
        /// Whether is a null value or not.
        /// </summary>
        internal bool HasValue { get { return Value != null; } }
        /// <summary>
        /// Initializaes a new instance of <see cref="ValueComparer"/> struct.
        /// </summary>
        /// <param name="value">Value to initialize.</param>
        /// <param name="fieldType">The field type.</param>
        internal ValueComparer(object value, EFieldType fieldType)
        {
            Value = value;
            FieldType = fieldType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator <(ValueComparer v1, ValueComparer v2)
        {
            bool result;
            switch (FieldType)
            {
                case EFieldType.Date:
                case EFieldType.DateTime:
                    result = (DateTime)v1.Value < (DateTime)v2.Value;
                    break;
                case EFieldType.Decimal:
                    result = (decimal)v1.Value < (decimal)v2.Value;
                    break;
                case EFieldType.Integer:
                    result = (long)v1.Value < (long)v2.Value;
                    break;
                case EFieldType.Time:
                    result = (TimeSpan)v1.Value < (TimeSpan)v2.Value;
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool operator >(ValueComparer v1, ValueComparer v2)
        {
            bool result;
            switch (FieldType)
            {
                case EFieldType.Date:
                case EFieldType.DateTime:
                    result = (DateTime)v1.Value > (DateTime)v2.Value;
                    break;
                case EFieldType.Decimal:
                    result = (decimal)v1.Value > (decimal)v2.Value;
                    break;
                case EFieldType.Integer:
                    result = (long)v1.Value > (long)v2.Value;
                    break;
                case EFieldType.Time:
                    result = (TimeSpan)v1.Value > (TimeSpan)v2.Value;
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Gets a formatted value.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        internal string GetFormattedValue(string format)
        {
            string result = null;
            if (this.HasValue)
            {
                if (!string.IsNullOrWhiteSpace(format))
                {
                    switch (FieldType)
                    {
                        case EFieldType.Date:
                        case EFieldType.DateTime:
                            result = ((DateTime)this.Value).ToString(format);
                            break;
                        case EFieldType.Decimal:
                            result = ((decimal)this.Value).ToString(format);
                            break;
                        case EFieldType.Integer:
                            result = ((long)this.Value).ToString(format);
                            break;
                        case EFieldType.Time:
                            result = ((TimeSpan)this.Value).ToString(format);
                            break;
                    }
                }
                else
                {
                    result = this.Value.ToString();
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Value?.ToString();
        }
    }
}

/*
    Copyright (c) 2013 Michael Johnson

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
 */

using System;
using System.Security.Cryptography;
using System.Text;

namespace DSInternals.Common.Cryptography
{
    /// <summary>
    /// Implements adaptable password-based key derivation functionality, PBKDF2, by using pseudo-random number generation based on a chosen System.Security.Cryptography.HMAC-derived hashing implementation.
    /// </summary>
    public sealed class Pbkdf2 : DeriveBytes
    {
        #region fields

        private uint _block;
        private int _blockSize;
        private byte[] _buffer;
        private int _endIndex;
        private string _hashName;
        private HMAC _hmac;
        private uint _iterationCount;
        private byte[] _password;
        private byte[] _salt;
        private int _startIndex;
        private int _state;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the System.Security.Cryptography.PBKDF2 class using a password, a salt, a number of iterations and the name of a System.Security.Cryptography.HMAC hashing implementation to derive the key.
        /// </summary>
        /// <param name="password">The password to derive the key for.</param>
        /// <param name="salt">The salt to use to derive the key.</param>
        /// <param name="iterations">The number of iterations to use to derive the key.</param>
        /// <param name="hashName">The name of the System.Security.Cryptography.HMAC implementation to use to derive the key.</param>
        /// <exception cref="System.ArgumentNullException">The password, salt or algorithm is null.</exception>
        /// <exception cref="System.ArgumentException">The salt size is less than 8 or the iterations is less than 1.</exception>
        public Pbkdf2(byte[] password, byte[] salt, int iterations, string hashName)
        {
            if (password == null)
                throw new ArgumentNullException("password");
            if (salt == null)
                throw new ArgumentNullException("salt");
            if (string.IsNullOrWhiteSpace(hashName))
                throw new ArgumentNullException("hashName");
            if (salt.Length < 8)
                throw new ArgumentException("Argument must be at least 8 bytes in length.", "salt");
            if (iterations < 1)
                throw new ArgumentException("Argument must be greater than zero.", "iterations");

            _password = (byte[])password.Clone();
            _salt = (byte[])salt.Clone();
            _hashName = hashName;
            _iterationCount = (uint)iterations;
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the System.Security.Cryptography.PBKDF2 class using a password, a salt, a number of iterations and the name of a System.Security.Cryptography.HMAC hashing implementation to derive the key.
        /// </summary>
        /// <param name="password">The password to derive the key for.</param>
        /// <param name="salt">The salt to use to derive the key.</param>
        /// <param name="iterations">The number of iterations to use to derive the key.</param>
        /// <param name="hashName">The name of the System.Security.Cryptography.HMAC implementation to use to derive the key.</param>
        /// <exception cref="System.ArgumentNullException">The password, salt or algorithm is null.</exception>
        /// <exception cref="System.ArgumentException">The salt size is less than 8 or the iterations is less than 1.</exception>
        public Pbkdf2(string password, byte[] salt, int iterations, string hashName)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("password");
            if (salt == null)
                throw new ArgumentNullException("salt");
            if (string.IsNullOrWhiteSpace(hashName))
                throw new ArgumentNullException("hashName");
            if (salt.Length < 8)
                throw new ArgumentException("Argument must be at least 8 bytes in length.", "salt");
            if (iterations < 1)
                throw new ArgumentException("Argument must be greater than zero.", "iterations");

            _password = new UTF8Encoding(false).GetBytes(password);
            _salt = (byte[])salt.Clone();
            _hashName = hashName;
            _iterationCount = (uint)iterations;
            Initialize();
        }

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the name of the hash algorithm to use when deriving the key.
        /// </summary>
        /// <returns>The name of the System.Security.Cryptography.HMAC hashing implementation used to derive the key.</returns>
        /// <exception cref="System.InvalidOperationException">value cannot be changed once the operation has begun.</exception>
        /// <exception cref="System.ArgumentNullException">value cannot be null, empty or consist of only whitespace characters.</exception>
        public string HashName
        {
            get { return _hashName; }
            set
            {
                if (_state != 0)
                    throw new InvalidOperationException("HashName value cannot be changed once the operation has begun.");
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("value");
                _hashName = value;
                Initialize();
            }
        }

        /// <summary>
        /// Gets or sets the number of iterations to use when deriving the key.
        /// </summary>
        /// <returns>The number of iterations used to derive the key.</returns>
        /// <exception cref="System.InvalidOperationException">value cannot be changed once the operation has begun.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">value must be greater than zero.</exception>
        public int IterationCount
        {
            get { return (int)_iterationCount; }
            set
            {
                if (_state != 0)
                    throw new InvalidOperationException("IterationCount value cannot be changed once the operation has begun.");
                if (value < 1)
                    throw new ArgumentOutOfRangeException("value", "value must be greater than zero.");
                _iterationCount = (uint)value;
                Initialize();
            }
        }

        /// <summary>
        /// Gets or sets the password to use when deriving the key.
        /// </summary>
        /// <returns>The password used to derive the key.</returns>
        /// <exception cref="System.InvalidOperationException">value cannot be changed once the operation has begun.</exception>
        /// <exception cref="System.ArgumentNullException">value cannot be null.</exception>
        /// <exception cref="System.ArgumentException">value must be at least 1 byte in length.</exception>
        public byte[] Password
        {
            get { return (byte[])_password.Clone(); }
            set
            {
                if (_state != 0)
                    throw new InvalidOperationException("Password value cannot be chance once the operation has begun.");
                if (value == null)
                    throw new ArgumentNullException("value");
                if (value.Length < 1)
                    throw new ArgumentException("Value must be a byte array at least 1 byte in length.", "value");
                _password = (byte[])value.Clone();
                Initialize();
            }
        }

        /// <summary>
        /// Gets or sets the salt to use when deriving the key.
        /// </summary>
        /// <returns>The salt used to derive the key.</returns>
        /// <exception cref="System.InvalidOperationException">value cannot be changed once the operation has begun.</exception>
        /// <exception cref="System.ArgumentNullException">value cannot be null.</exception>
        /// <exception cref="System.ArgumentException">value must be at least 8 bytes in length.</exception>
        public byte[] Salt
        {
            get { return (byte[])_salt.Clone(); }
            set
            {
                if (_state != 0)
                    throw new InvalidOperationException("Salt value cannot be changed once the operation has begun.");
                if (value == null)
                    throw new ArgumentNullException("value");
                if (value.Length < 8)
                    throw new ArgumentException("Value must be a byte array at least 8 bytes in length.", "value");
                _salt = (byte[])value.Clone();
                Initialize();
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Releases unmanaged resources used by the Syste.Security.Cryptography.PBKDF2 class and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resouces; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                if (_hmac != null)
                    _hmac.Dispose();
                if (_buffer != null)
                    Array.Clear(_buffer, 0, _buffer.Length);
                if (_password != null)
                    Array.Clear(_password, 0, _password.Length);
                if (_salt != null)
                    Array.Clear(_salt, 0, _salt.Length);
            }
        }

        // iterative hash function
        private byte[] Func()
        {
            byte[] INT_block = _block.GetBigEndianBytes();

            _hmac.TransformBlock(_salt, 0, _salt.Length, _salt, 0);
            _hmac.TransformFinalBlock(INT_block, 0, INT_block.Length);
            byte[] temp = _hmac.Hash;
            _hmac.Initialize();

            byte[] ret = temp;
            for (int i = 2; i <= _iterationCount; i++)
            {
                temp = _hmac.ComputeHash(temp);
                for (int j = 0; j < _blockSize; j++)
                {
                    ret[j] ^= temp[j];
                }
            }

            _block++;
            return ret;
        }

        /// <summary>
        /// Returns the pseudo-random bytes for this object.
        /// </summary>
        /// <param name="cb">The number of pseudo-random key bytes to generate.</param>
        /// <returns>A byte array filled with pseudo-random key bytes.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">cb must be greater than zero.</exception>
        /// <exception cref="System.ArgumentException">invalid start index or end index of internal buffer.</exception>
        public override byte[] GetBytes(int cb)
        {
            if (cb <= 0)
                throw new ArgumentOutOfRangeException("cb", "Argument must be a value greater than zero.");

            _state = 1;

            byte[] key = new byte[cb];
            int offset = 0;
            int size = _endIndex - _startIndex;
            if (size > 0)
            {
                if (cb >= size)
                {
                    Buffer.BlockCopy(_buffer, _startIndex, key, 0, size);
                    _startIndex = _endIndex = 0;
                    offset += size;
                }
                else
                {
                    Buffer.BlockCopy(_buffer, _startIndex, key, 0, cb);
                    _startIndex += cb;
                    return key;
                }
            }

            if (_startIndex != 0 && _endIndex != 0)
                throw new ArgumentException("Invalid start or end index in the internal buffer");

            while (offset < cb)
            {
                byte[] T_block = Func();
                int remainder = cb - offset;
                if (remainder > _blockSize)
                {
                    Buffer.BlockCopy(T_block, 0, key, offset, _blockSize);
                    offset += _blockSize;
                }
                else
                {
                    Buffer.BlockCopy(T_block, 0, key, offset, remainder);
                    offset += remainder;
                    Buffer.BlockCopy(T_block, remainder, _buffer, _startIndex, _blockSize - remainder);
                    _endIndex += (_blockSize - remainder);
                    return key;
                }
            }
            return key;
        }

        // initializes the state of the operation.
        private void Initialize()
        {
            if (_buffer != null)
                Array.Clear(_buffer, 0, _buffer.Length);

            _hmac = HMAC.Create(_hashName);
            _hmac.Key = (byte[])_password.Clone();
            _blockSize = _hmac.HashSize / 8;
            _buffer = new byte[_blockSize];
            _block = 1;
            _startIndex = _endIndex = 0;
            _state = 0;
        }

        /// <summary>
        /// Resets the state of the operation.
        /// </summary>
        public override void Reset()
        {
            Initialize();
        }

        #endregion
    }
}
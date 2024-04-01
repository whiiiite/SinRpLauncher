using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;
using System;
using System.IO;
using Launcher.Classes;

namespace SinRpLauncher.Util
{
    public static class UserDataUtil
    {
        /// <summary>
        /// Check nickname on valid.
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static bool NickNamePassedValidate(string nickname)
        {
            try
            {
                Regex pattern = new Regex(@"^([A-Z][A-Za-z]*_){1,2}[A-Z][A-Za-z]*$");
                return pattern.IsMatch(nickname);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// Make validation and return result of validation
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool NickNameIsValid(string nickname)
        {
            try
            {
                // return true if nickname is passed validation, or false if isn't
                return NickNamePassedValidate(nickname);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

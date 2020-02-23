public string Encrypt(string payload, string key)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(payload);

            var iv = new byte[16];
            new Random().NextBytes(iv);

            var bytes = Base64.Decode(key);
            var obj = new byte[32];
            Array.Copy(bytes, 7, obj, 0, Math.Min(bytes.Length, obj.Length));

            AesEngine engine = new AesEngine();
            CbcBlockCipher blockCipher = new CbcBlockCipher(engine);
            PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(blockCipher);
            KeyParameter keyParam = new KeyParameter(obj);
            ParametersWithIV keyParamWithIv = new ParametersWithIV(keyParam, iv, 0, 16);

            cipher.Init(true, keyParamWithIv);
            byte[] outputBytes = new byte[cipher.GetOutputSize(inputBytes.Length)];
            int length = cipher.ProcessBytes(inputBytes, outputBytes, 0);
            cipher.DoFinal(outputBytes, length); //Do the final block

            byte[] buffer = new byte[outputBytes.Length + iv.Length];

            Array.Copy(outputBytes, 0, buffer, 0, outputBytes.Length);
            Array.Copy(iv, 0, buffer, outputBytes.Length, iv.Length);

            string str = "2:" + Base64.ToBase64String(buffer);

            return "MatchFD51DE89D449_1 " + Base64.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

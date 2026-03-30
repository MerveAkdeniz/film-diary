import pandas as pd
import ast

file_path = "the-movies-dataset/credits.csv"
df = pd.read_csv(file_path)

print("OK: credits.csv okundu")
print("Satır:", df.shape[0])
print("Sütun:", df.shape[1])
print("Sütunlar:", list(df.columns))

print("\nİlk filmin cast verisi (HAM HALİ):\n")
raw_cast = df.iloc[0]["cast"]
print(raw_cast)

print("\nParse ediyoruz (ast.literal_eval):")
cast_list = ast.literal_eval(raw_cast)
print(cast_list[:3])

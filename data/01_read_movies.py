import pandas as pd

file_path = "the-movies-dataset/movies_metadata.csv"

df = pd.read_csv(file_path, low_memory=False)

print("OK: movies_metadata.csv okundu")
print("Satır:", df.shape[0])
print("Sütun:", df.shape[1])
print("İlk 10 sütun adı:", list(df.columns[:10]))
print("\nSütunların TAM LİSTESİ:")
for col in df.columns:
    print("-", col)

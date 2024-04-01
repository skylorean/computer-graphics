import React, { FC, useRef } from "react";
import Button from "../UI/Button/Button";
import styles from "./FilePicker.module.scss";

interface FilePickerProps {
  onChange: (files: FileList | null) => void;
}

const FilePicker: FC<FilePickerProps> = ({ onChange }) => {
  const fileInputRef = useRef<HTMLInputElement>(null);

  const handleButtonClick = () => {
    if (fileInputRef.current) {
      fileInputRef.current.click();
    }
  };

  const handleFileInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files) {
      const file = e.target.files[0];
      const reader = new FileReader();

      reader.onloadend = () => {
        // Проверяем тип файла
        if (reader.result && typeof reader.result === "string") {
          if (
            reader.result.startsWith("data:image/png") ||
            reader.result.startsWith("data:image/jpeg") ||
            reader.result.startsWith("data:image/bmp")
          ) {
            onChange(e.target.files);
          } else {
            alert("Пожалуйста, выберите файл PNG, JPEG или BMP.");
          }
        }
      };
      reader.readAsDataURL(file);
    }
  };

  return (
    <>
      <input
        type="file"
        className={styles["file-picker__input"]}
        ref={fileInputRef}
        onChange={handleFileInputChange}
        accept=".png,.jpeg,.jpg,.bmp"
      />
      <Button
        className={styles["file-picker__btn"]}
        onClick={handleButtonClick}
      >
        File
      </Button>
    </>
  );
};

export default FilePicker;

import { FC } from "react";
import styles from "./ImagePreview.module.scss";

interface ImagePreviewProps {
  imageSrc: string;
}

const ImagePreview: FC<ImagePreviewProps> = ({ imageSrc }) => {
  return (
    <div className={styles["image-preview"]}>
      <img src={imageSrc} alt="Selected" className={styles.image} />
    </div>
  );
};

export default ImagePreview;

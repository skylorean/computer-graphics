import { FC } from "react";
import styles from "./ImagePreview.module.scss";
import { SelectedImage } from "../../App";
import pngBackground from "../../assets/png-background.jpg";

interface ImagePreviewProps {
  selectedImage: SelectedImage;
}

const ImagePreview: FC<ImagePreviewProps> = ({ selectedImage }) => {
  const { extension, name, path, resolution } = selectedImage;
  const isExtensionPNG = extension === "png";

  return (
    <div className={styles["image-preview"]}>
      <div className={styles["image-container"]}>
        <div className={styles["image-wrapper"]}>
          <img src={path} alt="selected" className={styles.image} />

          {isExtensionPNG && (
            <img src={pngBackground} className={styles["aplha-layer"]} />
          )}
        </div>
      </div>
      <div className={styles["image-info"]}>
        <div className={styles["image-name"]}>
          {name}.{extension}
        </div>
        <div className={styles["image-original-resolution"]}>
          {resolution.width}×{resolution.height}
        </div>
      </div>
    </div>
  );
};

export default ImagePreview;

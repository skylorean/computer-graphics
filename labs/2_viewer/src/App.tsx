import { useState } from "react";
import Navbar from "./components/Navbar/Navbar";
import ImagePreview from "./components/ImagePreview/ImagePreview";

interface ImageResolution {
  width: number;
  height: number;
}

export interface SelectedImage {
  extension: string;
  name: string;
  path: string;
  resolution: ImageResolution;
}

function App() {
  // const [selectedImage, setSelectedImage] = useState<string | null>(null);
  const [selectedImage, setSelectedImage] = useState<SelectedImage | null>(
    null
  );

  const handleFileChange = (files: FileList | null) => {
    if (files && files.length > 0) {
      const file = files[0];
      const reader = new FileReader();
      console.log("file", file);
      console.log("reader", reader);

      reader.onloadend = () => {
        const [name, extension] = file.name.split(".");

        if (reader.result && typeof reader.result === "string") {
          const img = new Image();
          img.src = reader.result as string;

          img.onload = () => {
            setSelectedImage({
              path: img.src,
              extension: extension,
              name: name,
              resolution: { height: img.height, width: img.width },
            });
          };
        }
      };

      reader.readAsDataURL(file);
    }
  };

  return (
    <>
      <Navbar onImagePick={handleFileChange} />
      {selectedImage && <ImagePreview selectedImage={selectedImage} />}
    </>
  );
}

export default App;

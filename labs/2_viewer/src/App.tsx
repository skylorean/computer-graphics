import { useState } from "react";
import Navbar from "./components/Navbar/Navbar";
import ImagePreview from "./components/ImagePreview/ImagePreview";

function App() {
  const [selectedImage, setSelectedImage] = useState<string | null>(null);

  const handleFileChange = (files: FileList | null) => {
    if (files && files.length > 0) {
      const file = files[0];
      const reader = new FileReader();
      reader.onloadend = () => {
        if (reader.result && typeof reader.result === "string") {
          setSelectedImage(reader.result);
        }
      };
      reader.readAsDataURL(file);
    }
  };

  return (
    <>
      <Navbar onImagePick={handleFileChange} />
      {selectedImage && <ImagePreview imageSrc={selectedImage} />}
    </>
  );
}

export default App;

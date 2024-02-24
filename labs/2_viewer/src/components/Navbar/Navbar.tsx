import { FC } from "react";
import styles from "./Navbar.module.scss";
import FilePicker from "../FilePicker/FilePicker";

interface NavbarProps {
  onImagePick: (files: FileList | null) => void;
}

const Navbar: FC<NavbarProps> = ({ onImagePick }) => {
  return (
    <nav className={styles.navbar}>
      <FilePicker onChange={onImagePick} />
    </nav>
  );
};

export default Navbar;

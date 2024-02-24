import { FC } from "react";
import styles from "./Button.module.scss";

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  className?: string;
}

const Button: FC<ButtonProps> = ({ children, className, ...restProps }) => {
  const classnames2 = styles.btn + ` ${className ? className : ""}`;

  return (
    <button className={classnames2} {...restProps}>
      {children}
    </button>
  );
};

export default Button;

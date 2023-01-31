import * as Yup from "yup";

const passwordRegExp =
  /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_]).{6,}$/;
const phoneRegExp = /\(?([0-9]{3})\)?([ -]?)([0-9]{3})\2([0-9]{4})/;

export const RegistrationStepOneSchema = Yup.object().shape({
  firstName: Yup.string().required("Required").label("First name"),
  secondName: Yup.string().required("Required").label("Second name"),
  phone: Yup.string()
    .matches(phoneRegExp, "Phone must be (000)-000-0000")
    .label(`Phone number`),
  email: Yup.string()
    .email("Invalid email address")
    .required("Required")
    .label("Email address"),
});

export const RegistrationStepTwoSchema = Yup.object().shape({
  password: Yup.string()
    .min(6, "Password must be at least 6 characters")
    .required("Required")
    .matches(passwordRegExp, "Password must contains A-Z, a-z, 0-9")
    .label("Password"),
    
  confirmPassword: Yup.string()
    .min(6, "Password must be at least 6 characters")
    .required("Required")
    .matches(passwordRegExp, "Password must contains A-Z, a-z, 0-9")
    .label("Password"),
});

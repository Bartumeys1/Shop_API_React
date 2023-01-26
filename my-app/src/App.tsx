import React from "react";
import { Route, Routes } from "react-router-dom";
import "./App.css";
import LoginPage from "./components/auth/login";
import RegisterPage from "./components/auth/register";
import DefaultLayout from "./components/conteiners/default";

import HomePage from "./components/home";
import NoMatchPage from "./components/noMatch";
import CreateProduct from "./components/product/createProduct";


function App() {
  return (
    <Routes>
      <Route path="/" element ={<DefaultLayout/>}>
        <Route index element={<HomePage />} />
        <Route path="login" element={<LoginPage />} />
        <Route path="register" element={<RegisterPage />} />
        <Route path="add_product" element={<CreateProduct />} />

        <Route path="*" element={<NoMatchPage />} />
      </Route>
    </Routes>
  );
}

export default App;

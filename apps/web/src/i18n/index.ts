import i18n from "i18next";
import { initReactI18next } from "react-i18next";

import trCommon from "./locales/tr/common.json";
import enCommon from "./locales/en/common.json";

const savedLanguage = localStorage.getItem("app_language") || "tr";

i18n.use(initReactI18next).init({
  resources: {
    tr: {
      translation: trCommon,
    },
    en: {
      translation: enCommon,
    },
  },
  lng: savedLanguage,
  fallbackLng: "tr",
  interpolation: {
    escapeValue: false,
  },
  react: {
    useSuspense: false,
  },
});

export default i18n;

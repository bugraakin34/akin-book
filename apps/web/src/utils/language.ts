import i18n from "../i18n";

export async function changeLanguage(lang: "tr" | "en") {
  localStorage.setItem("app_language", lang);
  await i18n.changeLanguage(lang);
}

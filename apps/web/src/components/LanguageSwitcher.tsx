import { Select } from "antd";
import { useTranslation } from "react-i18next";

export default function LanguageSwitcher() {
  const { i18n, t } = useTranslation();

  const handleChange = async (lang: "tr" | "en") => {
    localStorage.setItem("app_language", lang);
    await i18n.changeLanguage(lang);
  };

  return (
    <Select
      value={i18n.language.startsWith("tr") ? "tr" : "en"}
      style={{ width: 140 }}
      onChange={(value) => handleChange(value as "tr" | "en")}
      options={[
        { value: "tr", label: t("language.turkish") },
        { value: "en", label: t("language.english") },
      ]}
    />
  );
}

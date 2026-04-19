import { useNavigate } from "react-router-dom";
import { login } from "../services/authService";
import { setAccessToken } from "../utils/storage";
import { Button, Card, Form, Input, message, Space, Typography } from "antd";
import { useTranslation } from "react-i18next";
import LanguageSwitcher from "../components/LanguageSwitcher";
import { useEffect } from "react";

export default function LoginPage() {
  const navigate = useNavigate();
  const { t } = useTranslation();

  useEffect(() => {
    const expired = localStorage.getItem("session_expired");

    if (expired === "true") {
      message.warning("Oturum süresi doldu, lütfen tekrar giriş yapın.");
      localStorage.removeItem("session_expired");
    }
  }, []);

  const onFinish = async (values: { email: string; password: string }) => {
    try {
      const result = await login(values);
      setAccessToken(result.accessToken);
      message.success(t("auth.loginSuccess"));
      navigate("/books");
    } catch {
      message.error(t("auth.loginError"));
    }
  };

  return (
    <div style={{ display: "flex", justifyContent: "center", marginTop: 80 }}>
      <Card style={{ width: 400 }}>
        <Space
          style={{
            width: "100%",
            justifyContent: "space-between",
            marginBottom: 16,
          }}
        >
          <Typography.Title level={3} style={{ margin: 0 }}>
            {t("auth.loginTitle")}
          </Typography.Title>

          <LanguageSwitcher />
        </Space>

        <Form layout="vertical" onFinish={onFinish}>
          <Form.Item
            label={t("auth.email")}
            name="email"
            rules={[{ required: true }]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            label={t("auth.password")}
            name="password"
            rules={[{ required: true }]}
          >
            <Input.Password />
          </Form.Item>

          <Button type="primary" htmlType="submit" block>
            {t("auth.login")}
          </Button>
        </Form>
      </Card>
    </div>
  );
}
